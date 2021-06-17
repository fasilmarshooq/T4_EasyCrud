using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.Model
{
    public interface IEntity
    {
        long Id { get; }
        bool IsNew { get; }
        long CreatedById { get; }
        DateTimeOffset? CreatedTime { get; }
        long? UpdatedById { get; }
        DateTimeOffset? UpdatedTime { get; }
        byte[] RowVersion { get; }

        void UpdateAuditFields(EntityState state);
    }
}
