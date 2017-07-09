using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.AspNetUsersBase;
    using Record = Models.AspNetUsersVM;
    using RecordModel = Models.AspNetUsers;
    using RecordSet = DbSet<Models.AspNetUsers>;
    using Context = Models.GesemEntities;
    using System.Text.RegularExpressions;
    using System;
    using System.Collections.Generic;
    using Models;
    using Microsoft.AspNet.SignalR;

    [HubName("AspNetUsersHub")]
    public class AspNetUsersHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.AspNetUsers;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            DateTime? startAlta = null, endAlta = null, startBaja = null, endBaja = null;
            if (columnFilters_5 != null)
            {
                string[] alta = columnFilters_5.Split(';');
                startAlta = DateTime.Parse(alta[0]);
                if (alta.Count() > 1)
                {
                    endAlta = DateTime.Parse(alta[1]);
                }
            }
            if (columnFilters_6 != null)
            {
                string[] baja = columnFilters_6.Split(';');
                startBaja = DateTime.Parse(baja[0]);
                if (baja.Count() > 1)
                {
                    endBaja = DateTime.Parse(baja[1]);
                }
            }
            var results = getRecordSet(context).Where(p => p.Id != 0 && // p.EstadoId != 12 &&  // 12 == Borrado
                     (search == null || (
                      p.UserName.StartsWith(search)
                   || p.Email.StartsWith(search)
                   || p.PhoneNumber.StartsWith(search)))
                      && (columnFilters_0 == null || p.Id.Equals(Id))
                      && (columnFilters_1 == null || (p.UserName.StartsWith(columnFilters_1)))
                      && (columnFilters_2 == null || (p.Email.StartsWith(columnFilters_2)))
                      && (columnFilters_3 == null || (p.PhoneNumber.StartsWith(columnFilters_3)))
                      && (startAlta == null || (p.FechaAlta >= startAlta))
                      && (endAlta == null || (p.FechaAlta <= endAlta))
                      && (startBaja == null || (p.FechaBaja >= startBaja))
                      && (endBaja == null || (p.FechaBaja <= endBaja))
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
            FindValidRegister(context, record, "Email");
            FindValidRegister(context, record, "UserName");
        }

        // Copia también el contenido de las relacciones many-many
        protected override void SuggestCompatibleRecordAndClone(Context context, RecordSet set, RecordModel record)
        {
            var Id = record.Id;
            base.SuggestCompatibleRecordAndClone(context, set, record);
            var orig = set.Find(Id);
            if (orig != null)
            {
                foreach (var val in orig.ApplicationGroups)
                {
                    record.ApplicationGroups.Add(val);
                }
                context.SaveChanges();
            }
        }

        protected override void SaveChanges(Context context, RecordModel record = null)
        {
            base.SaveChanges(context, record);
            if (record == null)
                return;
            if(SyncUserRoles(record))
                base.SaveChanges(context, record);
        }

        // Notificar todos los objetos foráneos que han sido afectados por el cambio
        protected override void AddForeignChanges(Context context, string change, RecordModel record, Record oldRecord = null)
        {
            var rolesKeys = record.ApplicationGroups.Select(r => r.Id);
            if (oldRecord != null)
            {
                rolesKeys = rolesKeys.Union(oldRecord.ApplicationGroupsIds);
            }
            AddChanges("ApplicationGroups", "change", rolesKeys.ToList());
        }

        // Sincroniza los roles de los grupos a los que pertenece el usuario con los roles que tiene asignado.
        public static bool SyncUserRoles(RecordModel record)
        {
            var set = new SortedDictionary<int, AspNetRoles>();
            foreach(var grp in record.ApplicationGroups)
            {
                foreach(var rol in grp.AspNetRoles)
                {
                    if(!set.ContainsKey(rol.Id))
                        set[rol.Id] = rol;
                }
            }
            var roles = record.AspNetRoles.Select(r => r.Id).OrderBy(i => i);
            var users = set.Keys.OrderBy(i => i);
            bool equal = users.SequenceEqual(roles);
            if (!equal)
            {
                record.AspNetRoles.Clear();
                foreach (var rol in set)
                {
                    record.AspNetRoles.Add(rol.Value);
                }
                //context.SaveChanges();
            }
            return !equal;
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