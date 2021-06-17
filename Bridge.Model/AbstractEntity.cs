using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bridge.Model
{
  
    public abstract class AbstractEntity : IEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTimeOffset? CreatedTime { get; set; }
        public long CreatedById { get; set; }
        public DateTimeOffset? UpdatedTime { get; set; }
        public long? UpdatedById { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public bool IsNew => Id == 0;

        public  void UpdateAuditFields(EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    CreatedById = 1;
                    CreatedTime = DateTime.Now;
                    break;

                case EntityState.Modified:
                    UpdatedById = 1;
                    UpdatedTime = DateTime.Now;
                    break;
            }
        }

    }

 
}
