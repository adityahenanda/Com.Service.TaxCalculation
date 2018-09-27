using AutoMapper;

namespace Com.Service.TaxCalculation.Lib.Facade.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, ProductViewModel>().ReverseMap();
        }
    }
}
