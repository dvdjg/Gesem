﻿using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.PresupuestosClientesDetalleBase;
    using Record = Models.PresupuestosClientesDetalleVM;
    using RecordModel = Models.PresupuestosClientesDetalle;
    using RecordSet = DbSet<Models.PresupuestosClientesDetalle>;
    using Context = Models.GesemEntities;
    using Microsoft.AspNet.SignalR;
    using System.Collections.Generic;
    using Models;

    [HubName("PresupuestosClientesDetalleHub")]
    public class PresupuestosClientesDetalleHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.PresupuestosClientesDetalle;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context);
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