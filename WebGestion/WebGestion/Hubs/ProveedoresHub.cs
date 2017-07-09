using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.ProveedoresBase;
    using Record = Models.ProveedoresVM;
    using RecordModel = Models.Proveedores;
    using RecordSet = DbSet<Models.Proveedores>;
    using Context = Models.GesemEntities;
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("ProveedoresHub")]
    public class ProveedoresHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.Proveedores;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => // p.Id != 0 && p.EstadoId != 12 &&  // 12 == Borrado
                (search == null || (
                p.TelefonoFAX.StartsWith(search)
             || p.CuentaBancaria.StartsWith(search)
             || p.Contacto.StartsWith(search)
             || p.Web.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || (p.TelefonoFAX.StartsWith(columnFilters_1)))
                && (columnFilters_2 == null || (p.CuentaBancaria != null && p.CuentaBancaria.StartsWith(columnFilters_2)))
                && (columnFilters_3 == null || (p.Contacto != null && p.Contacto.StartsWith(columnFilters_3)))
                && (columnFilters_4 == null || (p.Web != null && p.Web.StartsWith(columnFilters_4)))
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
            FindValidRegister(context, record, "Contacto");
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