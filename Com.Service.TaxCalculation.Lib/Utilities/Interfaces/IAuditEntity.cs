using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Utilities.Interfaces
{
    public interface IAuditEntity
    {
        /// Recording time against created entity. In UTC format
        DateTime CreatedUtc { get; set; }

        /// Recording user against created entity.
        string CreatedBy { get; set; }
    }
}
