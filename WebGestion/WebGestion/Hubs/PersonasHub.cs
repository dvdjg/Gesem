using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.PersonasBase;
    using Record = Models.PersonasVM;
    using RecordModel = Models.Personas;
    using RecordSet = DbSet<Models.Personas>;
    using Context = Models.GesemEntities;
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("PersonasHub")]
    public class PersonasHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.Personas;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => // p.Id != 0 && p.EstadoId != 12 &&  // 12 == Borrado
                (search == null || (
                p.Nombre.StartsWith(search)
             || p.Apellidos.StartsWith(search)
             || p.NIF.StartsWith(search)
             || p.Direccion.StartsWith(search)
             || p.Localidad.StartsWith(search)
             || p.Provincia.StartsWith(search)
             || p.Pais.StartsWith(search)
             || p.CP.StartsWith(search)
             || p.TelefonoFijo.StartsWith(search)
             || p.TelefonoMovil.StartsWith(search)
             || p.Email.StartsWith(search)
             || p.Observaciones.StartsWith(search)))
                && (columnFilters_0 == null || p.Id.Equals(Id))
                && (columnFilters_1 == null || (p.Nombre.StartsWith(columnFilters_1)))
                && (columnFilters_2 == null || (p.Apellidos != null && p.Apellidos.StartsWith(columnFilters_2)))
                && (columnFilters_3 == null || (p.NIF != null && p.NIF.StartsWith(columnFilters_3)))
                && (columnFilters_4 == null || (p.Direccion != null && p.Direccion.StartsWith(columnFilters_4)))
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
            FindValidRegister(context, record, "NIF");
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