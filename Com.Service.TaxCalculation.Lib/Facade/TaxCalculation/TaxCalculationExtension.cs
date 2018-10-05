using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public static class TaxCalculationExtension
    {
        public static string generateType(int taxCode)
        {
            string Type = "";

            if (taxCode == 1)
            {
                Type = "Food";

                return Type;
            }

            if (taxCode == 2)
            {
                Type = "Tobacco";

                return Type;
            }

            if (taxCode == 3)
            {
                Type = "Entertainment";

                return Type;
            }

            return Type;
        }

        public static double generateTaxAmount(int taxCode, TaxCalculationDetailsModel data)
        {

            if (taxCode.Equals(1))
            {
                data.TaxAmount = data.Amount * 0.1;
                return (double)data.TaxAmount;
            }

            if (taxCode.Equals(2))
            {

                data.TaxAmount =  10 + 0.02 * data.Amount;
                return (double)data.TaxAmount;
            }

            if (taxCode.Equals(3))
            {
                data.TaxAmount = data.Amount >= 100 ? (data.Amount - 100) * 0.01 : 0;
                return (double)data.TaxAmount;

            }

            return (double)data.TaxAmount;
        }
    }
}
