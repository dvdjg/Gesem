﻿using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.DireccionesFacturacionBase;
    using Record = Models.DireccionesFacturacionVM;
    using RecordModel = Models.DireccionesFacturacion;
    using RecordSet = DbSet<Models.DireccionesFacturacion>;
    using Context = Models.GesemEntities;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("DireccionesFacturacionHub")]
    public class DireccionesFacturacionHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.DireccionesFacturacion;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => // p.Id != 0 && p.EstadoId != 12 &&  // 12 == Borrado
               (search == null || (
                /*p.Clientes.Personas.Nombre.StartsWith(search)
             || */p.Nombre.StartsWith(search)
             || p.Apellidos.StartsWith(search)
             || p.Direccion.StartsWith(search)
             || p.CP.StartsWith(search)
             || p.Localidad.StartsWith(search)
             || p.Provincia.StartsWith(search)
             || p.Pais.StartsWith(search)
             || p.Observaciones.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || p.Nombre.StartsWith(columnFilters_1))
                && (columnFilters_2 == null || p.Apellidos.StartsWith(columnFilters_2))
                && (columnFilters_3 == null || p.Direccion.StartsWith(columnFilters_3))
                && (columnFilters_4 == null || p.CP.StartsWith(columnFilters_4))
                && (columnFilters_5 == null || p.Localidad.StartsWith(columnFilters_5))
                && (columnFilters_6 == null || p.Provincia.StartsWith(columnFilters_6))
                && (columnFilters_7 == null || p.Pais.StartsWith(columnFilters_7))
                && (columnFilters_7 == null || p.Observaciones.StartsWith(columnFilters_7))
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