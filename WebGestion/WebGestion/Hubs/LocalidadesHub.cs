using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.LocalidadesBase;
    using Record = Models.LocalidadesVM;
    using RecordModel = Models.Localidades;
    using RecordSet = DbSet<Models.Localidades>;
    using Context = Models.GesemEntities;
    using System;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("LocalidadesHub")]
    public class LocalidadesHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.Localidades;
        }

        //protected override string getSortOrder(string columnName)
        //{
        //    return columnName == "Pais" ? "Provincias.Paises.Nombre"
        //        : columnName == "Provincia" ? "Provincias.Nombre"
        //        : columnName;
        //}

        //protected override IQueryable<RecordModel> GetSortedObject(IQueryable<RecordModel> query, string columnName, string ascDesc = null)
        //{
        //    Expression<Func<RecordModel, string>> expr;
        //    var colName = columnName.ToLower();
        //    if (colName == "idioma")
        //    {
        //        expr = r => r.Idiomas.Codigo;
        //    }
        //    else if (colName == "pais")
        //    {
        //        expr = r => r.Provincias.Paises.Nombre;
        //    }
        //    else if (colName == "provincia")
        //    {
        //        expr = r => r.Provincias.Nombre;
        //    }
        //    else
        //    {
        //        return base.GetSortedObject(query, columnName, ascDesc);
        //    }
        //    return ascDesc == "desc" ? query.OrderByDescending(expr) : query.OrderBy(expr);
        //}

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => // p.Id != 0 &&
               (search == null || (
                p.Idiomas.Codigo.StartsWith(search)
             || p.Nombre.StartsWith(search)
             || p.Provincias.Nombre.StartsWith(search)
             || p.Provincias.Paises.Nombre.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || p.Idiomas.Codigo.Equals(columnFilters_1))
                && (columnFilters_2 == null || p.Nombre.StartsWith(columnFilters_2))
                && (columnFilters_3 == null || p.Provincias.Nombre.StartsWith(columnFilters_3))
                && (columnFilters_4 == null || p.Provincias.Paises.Nombre.StartsWith(columnFilters_4))
                );
            return results;
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