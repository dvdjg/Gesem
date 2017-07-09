using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.BienesBase;
    using Record = Models.BienesVM;
    using RecordModel = Models.Bienes;
    using RecordSet = DbSet<Models.Bienes>;
    using Context = Models.GesemEntities;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("BienesHub")]
    public class BienesHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.Bienes;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => // p.Id != 0 && p.EstadoId != 12 &&  // 12 == Borrado
               (search == null || (
                p.CodBien.StartsWith(search)
             || p.Nombre.StartsWith(search)
             || p.Descripcion.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || p.CodBien.StartsWith(columnFilters_1))
                && (columnFilters_2 == null || p.Nombre.StartsWith(columnFilters_2))
                && (columnFilters_3 == null || p.Descripcion.StartsWith(columnFilters_3))
                );
            return results;
        }

        protected override void FindValidRegister(Context context, RecordModel record)
        {
            FindValidRegister(context, record, "CodBien");
            FindValidRegister(context, record, "Nombre");
        }

        // Notificar todos los objetos foráneos que han sido afectados por el cambio
        protected override void AddForeignChanges(Context context, string change, RecordModel record, Record oldRecord = null)
        {
            AddChanges("Cualidades", "change", record.Cualidades.Select(r => r.Id).ToList());

            var cualidadesKeys = record.Cualidades.Select(r => r.Id);
            if (oldRecord != null)
            {
                cualidadesKeys = cualidadesKeys.Union(oldRecord.CualidadesIds);
            }
            AddChanges("Cualidades", "change", cualidadesKeys.ToList());
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