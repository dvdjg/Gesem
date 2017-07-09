using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace WebGestion.Hubs
{
    using ModelRecord = Models.IRecordParent;
    using RecordBasic = Models.ApplicationGroupsBase;
    using Record = Models.ApplicationGroupsVM;
    using RecordModel = Models.ApplicationGroups;
    using RecordSet = DbSet<Models.ApplicationGroups>;
    using Context = Models.GesemEntities;
    using System.Text.RegularExpressions;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using Models;

    [HubName("ApplicationGroupsHub")]
    //[Microsoft.AspNet.SignalR.Authorize(Roles = "Admin")]
    public class ApplicationGroupsHub : RecordsHub<ModelRecord, RecordBasic, Record, RecordModel, RecordSet, Context>
    {
        protected override RecordSet getRecordSet(Context context)
        {
            return context.ApplicationGroups;
        }

        protected override IQueryable<RecordModel> Filter(Context context, string search, int Id, string columnFilters_0, string columnFilters_1, string columnFilters_2, string columnFilters_3, string columnFilters_4, string columnFilters_5, string columnFilters_6, string columnFilters_7)
        {
            var results = getRecordSet(context).Where(p => p.Id != 0 && // p.EstadoId != 12 &&  // 12 == Borrado
                     (search == null || (
                      p.Nombre.StartsWith(search)
                   || p.Descripcion.StartsWith(search)))
                      && (columnFilters_0 == null || p.Id.Equals(Id))
                      && (columnFilters_1 == null || (p.Nombre.StartsWith(columnFilters_1)))
                      && (columnFilters_2 == null || (p.Descripcion.StartsWith(columnFilters_2)))
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

        // Copia también el contenido de las relacciones many-many
        protected override void SuggestCompatibleRecordAndClone(Context context, RecordSet set, RecordModel record)
        {
            var Id = record.Id;
            base.SuggestCompatibleRecordAndClone(context, set, record);
            //var newId = record.Id;
            var orig = set.Find(Id);
            if(orig != null)
            {
                foreach(var rol in orig.AspNetRoles)
                {
                    record.AspNetRoles.Add(rol);
                }
                foreach(var user in orig.AspNetUsers)
                {
                    record.AspNetUsers.Add(user); 
                }
                context.SaveChanges();
            }
        }

        //protected override void SaveChanges(Context context, RecordModel record = null)
        //{
        //    base.SaveChanges(context, record);
        //    if (record == null)
        //        return;
        //    foreach (var user in record.AspNetUsers)
        //    {
        //        if (AspNetUsersHub.SyncUserRoles(user))
        //        {
        //            AddChange("AspNetUsers", "change", user.Id);
        //            base.SaveChanges(context, record);
        //        }
        //    }
        //}

        protected override void NotifyChanges(Context context, bool bNotify = true)
        {
            //if (context != null)
            //{
            //    foreach (var item in m_lstNotifyToken)
            //    {
            //        if (item.model == "AspNetUsers" && item.change == "change")
            //        {
            //            foreach (var userid in item.ids)
            //            {
            //                var user = context.AspNetUsers.Find(userid);
            //                if (user != null && AspNetUsersHub.SyncUserRoles(user))
            //                {
            //                    AddChange("AspNetUsers", "change", user.Id);
            //                    base.SaveChanges(context, record);
            //                }
            //            }
            //        }
            //    }
            //}
            base.NotifyChanges(context, bNotify);
        }

        // Notificar todos los objetos foráneos que han sido afectados por el cambio
        protected override void AddForeignChanges(Context context, string change, RecordModel record, Record oldRecord = null)
        {
            var rolesKeys = record.AspNetRoles.Select(r => r.Id);
            var usersKeys = record.AspNetUsers.Select(r => r.Id);
            if (oldRecord != null)
            {
                rolesKeys = rolesKeys.Union(oldRecord.AspNetRolesIds);
                usersKeys = usersKeys.Union(oldRecord.AspNetUsersIds);
            }
            AddChanges("AspNetRoles", "change", rolesKeys.ToList());
            AddChanges("AspNetUsers", "change", usersKeys.ToList());
            foreach (var userid in usersKeys)
            {
                var user = context.AspNetUsers.Find(userid);
                if (user != null && AspNetUsersHub.SyncUserRoles(user))
                {
                    AddChange("AspNetUsers", "change", user.Id);
                    base.SaveChanges(context, record);
                }
            }
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