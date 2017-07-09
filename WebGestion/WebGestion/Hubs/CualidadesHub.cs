using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.CualidadesBase;
    using Record = Models.CualidadesVM;
    using RecordModel = Models.Cualidades;
    using RecordSet = DbSet<Models.Cualidades>;
    using Context = Models.GesemEntities;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("CualidadesHub")]
    public class CualidadesHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.Cualidades;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => // p.Id != 0 && p.EstadoId != 12 &&  // 12 == Borrado
               (search == null || (
                   p.TiposDeCualidades.Nombre.StartsWith(search)
                || p.Descripcion.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || p.TiposDeCualidades.Nombre.StartsWith(columnFilters_1))
                && (columnFilters_2 == null || p.Descripcion.StartsWith(columnFilters_2))
                );
            return results;
        }

        // Notificar todos los objetos foráneos que han sido afectados por el cambio
        protected override void AddForeignChanges(Context context, string change, RecordModel record, Record oldRecord = null)
        {
            var rolesKeys = record.Bienes.Select(r => r.Id);
            if (oldRecord != null)
            {
                rolesKeys = rolesKeys.Union(oldRecord.BienesIds);
            }
            AddChanges("Bienes", "change", rolesKeys.ToList());
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