using Com.Service.TaxCalculation.Lib.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Utilities
{
    public static class EntityExtension
    {
        public static void FlagForCreate(this IAuditEntity entity, string createdBy)
        {
            entity.CreatedBy = createdBy;
            entity.CreatedUtc = DateTime.UtcNow;
        }
    }
}
