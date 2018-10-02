using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public class TaxCalculationProfile : Profile
    {
        public TaxCalculationProfile()
        {
            CreateMap<TaxCalculationModel, TaxCalculationViewModel>().ReverseMap();
            CreateMap<TaxCalculationDetailsModel, TaxCalculationDetailsViewModel>().ReverseMap();
        }
    }
}
