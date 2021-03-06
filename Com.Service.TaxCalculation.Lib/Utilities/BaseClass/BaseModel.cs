﻿using Com.Service.TaxCalculation.Lib.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Utilities.BaseClass
{
    public class BaseModel : IAuditEntity
    {
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string CreatedBy { get; set; }
    }
}
