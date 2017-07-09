using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Data.Entity;
using WebGestion.Models;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.Reflection;
using System.Data.Entity.Validation;

namespace WebGestion.Hubs
{
    //[HubName("RecordsHub")]
    public abstract class RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context> : Hub 
        where ModelRecord: class, IRecordParent
        where RecordBasic : class, ICopyRecord, new()
        where Record: class, IIdRecord, ICopyRecord, new()
        where RecordModel: class, IIdRecord 
        where RecordSet: DbSet<RecordModel>, IQueryable<RecordModel>
        where Context : DbContext, new()
    {
        //protected Context context = new Context();
        protected static string innerException(Exception e)
        {
            string lastMessage = String.Empty;
            string msg = "<ul>";
            Exception ex = e;
            while (null != ex)
            {
                if (lastMessage != ex.Message)
                    msg += "<li>" + ex.Message + "</li>\n";
                lastMessage = ex.Message;
                ex = ex.InnerException;
            }
            msg += "</ul>";
            var exVal = e as DbEntityValidationException;
            if (exVal != null)
                foreach (var validator in exVal.EntityValidationErrors)
                    foreach (var ve in validator.ValidationErrors)
                        msg += "<dl><dt>" + ve.PropertyName + "</dt><dd>" + ve.ErrorMessage + "</dd></dl>\n";
            return msg;
        }

        protected virtual List<string> columnDistinct(Context context, string column, int start = 0, int length = -1)
        {
            //RecordSet rset =  getRecordSet(context);
            var modelName = typeof(RecordModel).Name;
            var ret = context.Database.SqlQuery<string>("SELECT DISTINCT @p0 FROM dbo." + modelName + " ORDER BY @p0 ASC", column).Skip(start);
            if (length >= 0)
                ret = ret.Take(length);
            return ret.ToList();
        }

        protected virtual void FindValidRegister(Context context, RecordModel record, string name)
        {
            var glass = new Utils.LookingGlass(record);
            var modelName = typeof(RecordModel).Name;
            for (var num = 1; num < 1000; ++num)
            {
                var strNombre = Regex.Replace((string)glass.GetMemberValue(name), @" \(\d+\)$", "") + " (" + num + ")";
                var found = context.Database.SqlQuery<string>("SELECT TOP 1 " + name + " FROM dbo." + modelName + " WHERE "+ name + "='"+ strNombre + "'");
                if (found.Count() == 0)
                {
                    glass.SetMember(name, strNombre);
                    break;
                }
            }
        }

        protected virtual List<string> columnDistinct(string column)
        {
            using (var context = new Context())
            {
                return columnDistinct(context, column);
            }
        }
        
        protected int countTotal { get; set; }
        protected abstract RecordSet getRecordSet(Context context);
        protected class LastQuerySearch
        {
            public string lastFindToken { get; set; }
            public int countTotal { get; set; }
        }
        protected List<LastQuerySearch> lastQuerySearch = new List<LastQuerySearch>();

        protected virtual IQueryable<RecordModel> GetSortedObject(IQueryable<RecordModel> query, Dictionary<string, string> orderNameDesc)
        {
            string sortBy = "";
            foreach (var item in orderNameDesc)
            {
                if (sortBy.Count() > 0)
                    sortBy += ", ";
                var columnName = item.Key;
                sortBy += columnName;
                if (item.Value.Count() > 0)
                    sortBy += ' ' + item.Value;
            }
            return query.SortBy(sortBy);
        }

        protected virtual IQueryable<RecordModel> GetResultObject(
            Context context,
            //out int countTotal,
            string search,
            Dictionary<string, string> orderNameDesc,
            int start,
            int length,
            List<string> columnFilters
            )
        {
            var res = FilterResult(context, search, columnFilters);
            if (countTotal < 0)
                countTotal = res.Count();
            if (orderNameDesc != null && orderNameDesc.Count > 0)
            {
                res = GetSortedObject(res, orderNameDesc);
                res = res.Skip(start);
            }
            else
            {
                Console.WriteLine(String.Format("No se ha encontrado columnas para ordenar.\nsearch={0}", search));
            }
            return res.Take(length);
        }

        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        /// <summary>
        /// Devuelve los registros cuyas claves hayan sido proporcionadas.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual IEnumerable<Record> GetRecordsById(List<int> ids)
        {
            LogIdentity(System.Reflection.MethodInfo.GetCurrentMethod());

            var ret = new List<Record>();
            using (var context = new Context())
            {
                var set = getRecordSet(context);

                foreach (var id in ids)
                {
                    var rec = set.Find(id);
                    if (rec == null)
                        continue;
                    var record = new Record();
                    record.CopyFrom(rec);
                    ret.Add(record);
                }
            }
            return ret;
        }

        protected virtual void LogIdentity(MethodBase from)
        {
            var user = this.Context.User;
            if (user != null)
            {
                var uname = user.Identity.IsAuthenticated ? user.Identity.Name : "anonymous";
                var modelname = typeof(RecordModel).Name;
                Debug.WriteLine("[" + uname + "] "+ from.Name + "<" + modelname + ">()");
            }
        }

        /// <summary>
        /// Devuelve los registros que se pidan.
        /// </summary>
        /// <param name="param">Parámetro de Datatables para especificar una serie de registros con filtrado incluido.</param>
        /// <returns></returns>
        public virtual DTResultHub<Record> GetRecords(DTParameters param = null)
        {
            DTResultHub<Record> ret = null;
            try
            {
                LogIdentity(System.Reflection.MethodInfo.GetCurrentMethod());

                var sw = Stopwatch.StartNew();
                List<string> columnSearch = new List<string>();

                if (param == null)
                    param = new DTParameters();
                
                if (param.Columns != null)
                {
                    foreach (var column in param.Columns)
                    {
                        if (column.Searchable)
                        {
                            var value = column.Search.Value;
                            columnSearch.Add(value == "" ? null : (value.StartsWith("^") && value.EndsWith("$")) ? value.Substring(1, value.Length - 2) : value); // + '%'
                        }
                    }
                }
                var orderNameDesc = new Dictionary<string, string>();
                if (param.Columns != null && param.Order != null) // && param.Order.Length > 0
                {
                    foreach (var order in param.Order)
                    {
                        var nColumn = order.Column;
                        var column = param.Columns[nColumn];
                        if (column.Orderable)
                        {
                            var ascDesc = order.Dir.ToLower();
                            if (ascDesc != "desc")
                                ascDesc = "";
                            orderNameDesc[column.Data] = ascDesc;
                        }
                    }
                }
                if (orderNameDesc.Count == 0)
                {
                    // Si no se ha especificado correctamente al menos una columna para ordenar, buscar la primera que sea ordenable
                    foreach (var column in param.Columns)
                    {
                        if (column.Orderable)
                        {
                            orderNameDesc[column.Data] = "";
                            break;
                        }
                    }
                }

                var paramSearch = new { search = param.Search, order = param.Order, columns = param.Columns };
                var lastFindToken = serializer.Serialize(paramSearch);
                using (var context = new Context())
                {
                    var last = lastQuerySearch.Find(x => x.lastFindToken == lastFindToken);
                    if (last == null)
                        countTotal = -1;
                    var data = GetResultObject(
                        context, //out countTotal,
                        param.Search.Value,
                        orderNameDesc,
                        param.Start,
                        param.Length,
                        columnSearch
                    );

                    var dataList = data.ToArray().Select(
                        s => (Record)(new Record()).CopyFrom(s)
                        ).ToList();
                    //var dataList = getDataList(data);
                    if (last == null && countTotal >= 0)
                    {
                        lastQuerySearch.Insert(0, new LastQuerySearch() { countTotal = countTotal, lastFindToken = lastFindToken });
                        var idx = lastQuerySearch.Count;
                        if (idx > 5)
                            lastQuerySearch.RemoveAt(idx - 1);
                    }

                    var queryTokenType = new { recordsTotal = countTotal, recordsFiltered = countTotal, search = param.Search, order = param.Order, columns = param.Columns };
                    //var queryToken = serializer.Serialize(queryTokenType).ToLower();

                    var elapsed = sw.Elapsed;

                    //int count = RecordModel.Count(dtsource, param.Search.Value, columnSearch);
                    //var json = GetJsonResult(data);
                    var name = typeof(RecordModel).Name;
                    ret = new DTResultHub<Record>
                    {
                        name = name,
                        queryToken = queryTokenType,
                        //queryToken = "(" + dataList.Count.ToString() + '/' + countTotal.ToString() + ") " + lastFindToken,
                        start = param.Start,
                        draw = param.Draw,
                        data = dataList,
                        recordsFiltered = countTotal, // data.Count(),
                        recordsTotal = countTotal,
                        elapsed = elapsed.TotalMilliseconds
                    };
                    //Clients.Caller.recordRange(result);
                    //ret = true;
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.reportError("No se han podido listar registros:\n" + innerException(ex));
            }
            return ret;
        }

        /// <summary>
        /// Función a ser implementada en clases derivadas con la sentencia de búsqueda en Base de Datos
        /// </summary>
        /// <param name="context"></param>
        /// <param name="search"></param>
        /// <param name="Id"></param>
        /// <param name="columnFilters_0"></param>
        /// <param name="columnFilters_1"></param>
        /// <param name="columnFilters_2"></param>
        /// <param name="columnFilters_3"></param>
        /// <param name="columnFilters_4"></param>
        /// <param name="columnFilters_5"></param>
        /// <param name="columnFilters_6"></param>
        /// <param name="columnFilters_7"></param>
        /// <returns></returns>
        protected abstract IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7);

        /// <summary>
        /// Filtra los resultados de la query
        /// </summary>
        /// <param name="context"></param>
        /// <param name="search"></param>
        /// <param name="columnFilters"></param>
        /// <returns></returns>
        protected virtual IQueryable<RecordModel> FilterResult(Context context, string search, List<string> columnFilters)
        {
            int Id = -1;
            string columnFilters_0 = null, columnFilters_1 = null, columnFilters_2 = null, columnFilters_3 = null, columnFilters_4 = null, columnFilters_5 = null, columnFilters_6 = null, columnFilters_7 = null;
            if (search == string.Empty)
                search = null;
            if (search != null)
                search = search.ToLower();
            if (columnFilters != null)
            {
                if (columnFilters.Count > 0)
                    columnFilters_0 = columnFilters[0];
                if (columnFilters.Count > 1)
                    columnFilters_1 = columnFilters[1];
                if (columnFilters.Count > 2)
                    columnFilters_2 = columnFilters[2];
                if (columnFilters.Count > 3)
                    columnFilters_3 = columnFilters[3];
                if (columnFilters.Count > 4)
                    columnFilters_4 = columnFilters[4];
                if (columnFilters.Count > 5)
                    columnFilters_5 = columnFilters[5];
                if (columnFilters.Count > 6)
                    columnFilters_6 = columnFilters[6];
                if (columnFilters.Count > 7)
                    columnFilters_7 = columnFilters[7];
            }
            if (columnFilters_0 != null)
                Id = int.Parse(columnFilters_0);

            var results = Filter(context, search, Id, columnFilters_0, columnFilters_1, columnFilters_2, columnFilters_3, columnFilters_4, columnFilters_5, columnFilters_6, columnFilters_7);

            return results;
        }

        /// <summary>
        /// Encuentra un registro que se parezca a record y que pueda ser susceptible de ser insertado en la base de datos sin que colisone con otro.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="record"></param>
        protected virtual void FindValidRegister(Context context, RecordModel record)
        {
            // Por defecto no se hacen cambios
        }

        /// <summary>
        /// Almacena información del registro para que el cliente pueda saber de qué se compone y de qué otras tablas depende.
        /// </summary>
        public class HubInfo
        {
            public DateTime now = DateTime.Now;
            public RecordBasic blankRecord = new RecordBasic();
            public SortedDictionary<string, List<Dictionary<string, object>>> recordProperties;

            public HubInfo()
            {
                recordProperties = DescribeType(typeof(Record)); // Incluidas propiedades derivadas
            }

            private static object listAdaptor(IEnumerable<object> obj)
            {
                if (obj.Count() == 0)
                    return null;
                else if (obj.Count() == 1)
                    return obj.ElementAt(0);
                else
                    return obj;
            }

            public static SortedDictionary<string, List<Dictionary<string, object>>> DescribeType(Type type)
            {
                var ret = new SortedDictionary<string, List<Dictionary<string, object>>>();
                if (type == null)
                    return ret;
                //PropertyInfo[] props = type.GetProperties();
                // Forzar un orden en concreto según el atributo [Order]
                var props = from property in type.GetProperties()
                                 let orderAttribute = property.GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() as OrderAttribute
                                 orderby orderAttribute == null ? 0 : orderAttribute.Order
                                 select property;
                foreach (PropertyInfo prp in props)
                {
                    var prptype = prp.PropertyType;
                    var underlyingType = Nullable.GetUnderlyingType(prptype);
                    var returnType = underlyingType ?? prptype;
                    var enumType = prp.PropertyType == typeof(String) ? null : Utils.Reflection.GetEnumerableType(returnType);
                    returnType = enumType ?? returnType;

                    var o = new Dictionary<string, object>();
                    o["Name"] = prp.Name;
                    o["Type"] = returnType.Name; // + (enumType != null ? "[]" : "");
                    o["Nullable"] = (underlyingType != null) ? true : false;
                    o["Array"] = (enumType != null) ? true : false;
                    //o["Declaring"] = prp.DeclaringType.Name;
                    var data = prp.GetCustomAttributesData();
                    o["Attribute"] = data.ToDictionary(
                        p => p.Constructor.DeclaringType.Name,
                        p => p.ConstructorArguments.Count > 0 ? listAdaptor(p.ConstructorArguments.Select(l => l.Value)) :
                            p.NamedArguments.Count > 0 ? p.NamedArguments.ToDictionary(l => l.MemberName, l => l.TypedValue.Value) as object : null);
                    if (!ret.ContainsKey(prp.DeclaringType.Name))
                        ret[prp.DeclaringType.Name] = new List<Dictionary<string, object>>();
                    ret[prp.DeclaringType.Name].Add(o); 
                }
                return ret;
            }
        }

        public virtual HubInfo GetInfo()
        {
            LogIdentity(System.Reflection.MethodInfo.GetCurrentMethod());

            var info = new HubInfo();
            return info;
        }

        /// <summary>
        /// Añade un nuevo registro a la colección
        /// </summary>
        /// <param name="newRecord"></param>
        /// <returns></returns>
        public virtual bool Add(Record newRecord)
        {
            LogIdentity(System.Reflection.MethodInfo.GetCurrentMethod());

            var ret = false;
            try
            {
                using (var context = new Context())
                {
                    var set = getRecordSet(context);
                    var record = set.Create();
                    newRecord.CopyTo(context, record);
                    //if(record.Id == 0) // Si se intenta introducir un índice 0, dejar que SQL Server decida el que corresponda
                    //    record.Id = -1;
                    set.Add(record);
                    //context.SaveChanges();
                    SaveChanges(context, record);
                    AddForeignChanges(context, "add", record); // Añadir aquí porque ya se ha adjudicado el Id definitivo
                    AddChange("add", record); // Añadir aquí porque ya se ha adjudicado el Id definitivo
                    NotifyChanges(context);
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                NotifyChanges(null, false); // Descarta las notificaciones
                Clients.Caller.reportError("No se puede añadir el registro:" + "\n" + innerException(ex));
            }
            return ret;
        }

        protected virtual void SaveChanges(Context context, RecordModel record = null) { context.SaveChanges(); }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        public virtual bool Update(Record updatedRecord)
        {
            LogIdentity(System.Reflection.MethodInfo.GetCurrentMethod());

            var ret = false;
            try
            {
                if (updatedRecord.Id == 0) // Que no se pueda actualizar
                    throw new Exception("No se permite modificar el registro raíz.");
                using (var context = new Context())
                {
                    var record = getRecordSet(context).FirstOrDefault(t => t.Id == updatedRecord.Id);
                    if (record != null)
                    {

                        AddForeignChanges(context, "update", record, updatedRecord);
                        updatedRecord.CopyTo(context, record);
                        AddChange("update", record);
                        SaveChanges(context, record);
                        ret = true;
                        // Notificar el nuevo valor del registro o su anterior valor en caso de que no se haya podido actualizar
                        NotifyChanges(context); 
                    }
                }
            }
            catch (Exception ex)
            {
                NotifyChanges(null, false); // Descarta las notificaciones
                var err = "No se ha podido actualizar el registro.\n" + innerException(ex);
                Clients.Caller.reportError(err);
            }
            return ret;
        }

        /// <summary>
        /// Borra registros cuyos ids se pasen en el argumento.
        /// Notifica a todos los clientes la lista de los registros que se han borrado.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual bool Delete(List<int> ids)
        {
            LogIdentity(System.Reflection.MethodInfo.GetCurrentMethod());

            using (var context = new Context())
            {
                using (var contextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var set = getRecordSet(context);
                        foreach (var Id in ids)
                        {
                            if (Id != 0)
                            {
                                var record = set.FirstOrDefault(t => t.Id == Id);
                                AddForeignChanges(context, "delete", record);
                                AddChange("delete", record); // Idealmente se notificaría después de salvar los cambios
                                set.Remove(record);
                                //context.SaveChanges();
                                SaveChanges(context, record);
                            }
                            else
                            {
                                throw new Exception("No se permite borrar el registro raíz.");
                            }
                        }
                        contextTransaction.Commit();
                        NotifyChanges(context);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        NotifyChanges(null, false); // Descarta las notificaciones
                        contextTransaction.Rollback();
                        Clients.Caller.reportError("No se han podido eliminar registros:<br />" + innerException(ex));
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Clona registros.
        /// Notifica a todos los clientes la lista de los nuevos registros que se han creado.
        /// Puede usarse la función GetRecordsById para recuperar el contenido de dichos registros (si el usuario tiene permisos para verlos)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual bool Clone(List<int> ids)
        {
            LogIdentity(System.Reflection.MethodInfo.GetCurrentMethod());

            using (var context = new Context())
            {
                using (var contextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var set = getRecordSet(context);
                        foreach (var Id in ids)
                        {
                            var record = set.AsNoTracking().FirstOrDefault(t => t.Id == Id);
                            if (record != null)
                            {
                                // Ctrl+R followed by Ctrl+M
                                SuggestCompatibleRecordAndClone(context, set, record);
                                AddForeignChanges(context, "clone", record);
                                AddChange("clone", record);
                                //context.SaveChanges();
                                SaveChanges(context, record);
                            }
                        }
                        contextTransaction.Commit();
                        //Clients.All.recordsAdded(newIds);
                        NotifyChanges(context);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        NotifyChanges(null, false); // Descarta las notificaciones
                        contextTransaction.Rollback();
                        Clients.Caller.reportError("No se han podido clonar registros:\n" + innerException(ex));
                        return false;
                    }
                }
            }
        }

        protected class NotifyToken
        {
            public string model;
            public string change;
            public IEnumerable<int> ids;
        }

        protected List<NotifyToken> m_lstNotifyToken = new List<NotifyToken>();
        protected virtual void AddForeignChanges(Context context, string change, RecordModel record, Record oldRecord = null)
        {
        }

        protected virtual void AddChange(string change, RecordModel record)
        {
            var modelName = typeof(RecordModel).Name;
            AddChange(modelName, change, record.Id);
        }

        protected virtual void AddChange(string model, string change, int id)
        {
            int[] ids = { id };
            AddChanges(model, change, ids);
        }

        protected virtual void AddChanges(string model, string change, IEnumerable<int> ids)
        {
            if (ids.Any())
            {
                var token = new NotifyToken() { model = model, change = change, ids = ids };
                m_lstNotifyToken.Add(token);
            }
        }

        protected virtual void NotifyChanges(Context context, bool bNotify = true)
        {
            if (bNotify)
            {
                var modelName = typeof(RecordModel).Name;
                foreach (var item in m_lstNotifyToken)
                {
                    Clients.All.recordsChanged(item.model, item.change, item.ids);
                    //if (modelName == item.model)
                    //{
                    //    Clients.Caller.recordsChanged(item.model, item.change, item.ids);
                    //}
                    //else
                    //{
                    //    Clients.All.recordsChanged(item.model, item.change, item.ids);
                    //}
                }
            }
            m_lstNotifyToken.Clear();
        }

        // En las clases derivadas copia el contenido de las relaciones
        protected virtual void SuggestCompatibleRecordAndClone(Context context, RecordSet set, RecordModel record)
        {
            FindValidRegister(context, record);
            set.Add(record);
            context.SaveChanges();
        }

        protected void SendRecordRange(RecordSet set, int start)
        {
            try
            {
                var name = typeof(RecordModel).Name;
                var res = set.Where(r => r.Id != 0).ToArray().Select(r => (new Record()).CopyFrom(r) as Record).ToList();
                var result = new DTResultHub<Record> { data = res, name = name, start = start };
                Clients.Caller.recordRange(result);
            }
            catch (Exception ex)
            {
                Clients.Caller.reportError("No se han podido listar registros:\n" + innerException(ex));
            }
        }
    }
}
