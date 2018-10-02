using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public static class TaxCalculationExtension
    {
        public static string generateType(int taxCode)
        {
            switch (taxCode)
            {
                default:
                    return "Food";
                case 2:
                    return "Tobacco";
                case 3:
                    return "Entertainment";
            }
        }

        public static double generateTaxAmount(int taxCode, TaxCalculationDetailsModel data)
        {
            switch (taxCode)
            {
                default:
                    data.TaxAmount = (10 / 100) * data.Amount;
                    return (double)data.TaxAmount;
                case 2:
                    data.TaxAmount = 10 + (2 / 100 * data.Amount);
                    return (double)data.TaxAmount;
                case 3:
                    data.TaxAmount = data.Amount >= 100 ? (1 / 100) * (data.Amount - 100) : 0;
                    return (double)data.TaxAmount;
            }
        }
    }
}
