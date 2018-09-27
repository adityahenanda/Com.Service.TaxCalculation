using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Utilities.Interfaces
{
    public interface IAuditEntity
    {
        /// <summary>
        /// Recording time against created entity. In UTC format
        /// </summary>
        /// <returns></returns>
        DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Recording user against created entity.
        /// </summary>
        /// <returns></returns>
        string CreatedBy { get; set; }
    }
}
