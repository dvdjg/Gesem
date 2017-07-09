using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.CambiosLogBase;
    using Record = Models.CambiosLogVM;
    using RecordModel = Models.CambiosLog;
    using RecordSet = DbSet<Models.CambiosLog>;
    using Context = Models.GesemEntities;
    using System;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("CambiosLogHub")]
    public class CambiosLogHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.CambiosLog;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            DateTime? startAlta = null, endAlta = null;
            if (columnFilters_2 != null)
            {
                string[] alta = columnFilters_2.Split(';');
                startAlta = DateTime.Parse(alta[0]);
                if (alta.Count() > 1)
                {
                    endAlta = DateTime.Parse(alta[1]);
                }
            }
            var results = getRecordSet(context).Where(p => // p.Id != 0 && p.EstadoId != 12 &&  // 12 == Borrado
               (search == null || (
                p.Query.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || p.Query.StartsWith(columnFilters_1))
                && (startAlta == null || (p.Fecha >= startAlta))
                && (endAlta == null || (p.Fecha <= endAlta))
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