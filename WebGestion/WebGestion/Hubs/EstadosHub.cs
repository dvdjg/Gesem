using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.EstadosBase;
    using Record = Models.EstadosVM;
    using RecordModel = Models.Estados;
    using RecordSet = DbSet<Models.Estados>;
    using Context = Models.GesemEntities;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("EstadosHub")]
    public class EstadosHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.Estados;
        }

        protected override IQueryable<RecordModel> GetSortedObject(IQueryable<RecordModel> query, Dictionary<string, string> orderNameDesc)
        {
            string sortBy = "";
            foreach (var item in orderNameDesc)
            {
                if (sortBy.Count() > 0)
                    sortBy += ", ";
                var columnName = item.Key;
                if (columnName == "Padre")
                {
                    if (item.Value == "desc")
                        return query.OrderByDescending(r => r.Estados2.Nombre);
                    else
                        return query.OrderBy(r => r.Estados2.Nombre);
                }
                sortBy += columnName;
                if (item.Value.Count() > 0)
                    sortBy += ' ' + item.Value;
            }
            return query.SortBy(sortBy);
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => // p.Id != 0 && p.EstadoId != 12 &&  // 12 == Borrado
                (search == null || (
                p.Nombre.StartsWith(search)
            || p.Descripcion.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || (p.Nombre.StartsWith(columnFilters_1)))
                && (columnFilters_2 == null || (p.Descripcion != null && p.Descripcion.StartsWith(columnFilters_2)))
                && (columnFilters_3 == null || (p.PadreId.ToString().Equals(columnFilters_3)))
                && (columnFilters_4 == null || (p.Estados2.Nombre != null && p.Estados2.Nombre.StartsWith(columnFilters_4)))
                );
            return results;
        }

        /// <summary>
        /// Adapta el contenido del registro para que pudiera insertarse en base de datos sin colisiones
        /// </summary>
        /// <param name="set"></param>
        /// <param name="record"></param>
        protected override void FindValidRegister(Context context, RecordModel record)
        {
            FindValidRegister(context, record, "Nombre");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Métodos públicos
        //
        //[Authorize(Roles = "Admin,Consultar Artículos")]
        public override IEnumerable<Record> GetRecordsById(List<int> ids)
        {
            return base.GetRecordsById(ids);
        }

        //[Authorize(Roles = "Admin,Consultar Artículos")]
        public override DTResultHub<Record> GetRecords(DTParameters param = null)
        {
            return base.GetRecords(param);
        }

        [Authorize(Roles = "Admin")]
        public override bool Add(Record newRecord)
        {
            return base.Add(newRecord);
        }

        [Authorize(Roles = "Admin")]
        public override bool Update(Record updatedRecord)
        {
            return base.Update(updatedRecord);
        }

        [Authorize(Roles = "Admin")]
        public override bool Delete(List<int> ids)
        {
            return base.Delete(ids);
        }

        [Authorize(Roles = "Admin")]
        public override bool Clone(List<int> ids)
        {
            return base.Clone(ids);
        }
    }
}