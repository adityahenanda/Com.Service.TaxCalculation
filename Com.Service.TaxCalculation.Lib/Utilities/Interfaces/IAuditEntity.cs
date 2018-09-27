using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Utilities.Interfaces
{
    public interface IAuditEntity
    {

        DateTime CreatedUtc { get; set; }
        string CreatedBy { get; set; }
    }
}
