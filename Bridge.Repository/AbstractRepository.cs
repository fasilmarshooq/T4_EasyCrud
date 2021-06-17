using Bridge.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;


namespace Bridge.Data
{
    public class AbstractRepository : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseLazyLoadingProxies().UseSqlServer(@"Server=.;Database=EFtest;Trusted_Connection=True;");
     

        public override int SaveChanges()
        {
           
            UpdateAuditFields();
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException concurrencyEx)
            {
                //
                return -1;
            }

        }
        private void UpdateAuditFields()
        {

            var eligibleEntries = ChangeTracker.Entries<IEntity>()
                                    .Where(x=>(x.State == EntityState.Added || (x.State == EntityState.Modified))).ToList();

            foreach (var entry in eligibleEntries)
            {
                if (entry.Entity is IEntity entity)
                    entity.UpdateAuditFields(entry.State);
            }
        }
    }
}
