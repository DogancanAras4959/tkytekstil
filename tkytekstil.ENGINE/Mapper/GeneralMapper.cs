using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Dtos.BrandData;
using tkytekstil.ENGINE.Dtos.CategoryData;
using tkytekstil.ENGINE.Dtos.CityData;
using tkytekstil.ENGINE.Dtos.ColorData;
using tkytekstil.ENGINE.Dtos.ColorProductData;
using tkytekstil.ENGINE.Dtos.ContactDataData;
using tkytekstil.ENGINE.Dtos.ImagesProductData;
using tkytekstil.ENGINE.Dtos.OrderData;
using tkytekstil.ENGINE.Dtos.OrderProductData;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.ProductFavoriteData;
using tkytekstil.ENGINE.Dtos.ProvinceData;
using tkytekstil.ENGINE.Dtos.RoleData;
using tkytekstil.ENGINE.Dtos.ShoppersData;
using tkytekstil.ENGINE.Dtos.SizeData;
using tkytekstil.ENGINE.Dtos.SizeNumProductData;
using tkytekstil.ENGINE.Dtos.UserData;

namespace tkytekstil.ENGINE.Mapper
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<Roles, RoleDto>();
            CreateMap<RoleDto, Roles>();

            CreateMap<Users, UserDto>().ForMember(x=> x.role, y => y.MapFrom(t => t.role));
            CreateMap<UserDto, Users>();

            CreateMap<Categories, CategoriesDto>();
            CreateMap<CategoriesDto, Categories>();

            CreateMap<Brands, BrandDto>();
            CreateMap<BrandDto, Brands>();

            CreateMap<SizeNum, SizeDto>();
            CreateMap<SizeDto, SizeNum>();

            CreateMap<Shoppers, ShoppersDto>()
                .ForMember(x=> x.city, y=> y.MapFrom(t => t.cityShopper))
                .ForMember(x=> x.province, y=> y.MapFrom(t=> t.provinceShopper));
            CreateMap<ShoppersDto, Shoppers>();

            CreateMap<Products, ProductDto>()
              .ForMember(x => x.categoryProduct, y => y.MapFrom(t => t.categoryProduct))
              .ForMember(x => x.brandProduct, y => y.MapFrom(t => t.brandProduct));
            CreateMap<ProductDto, Products>();

            CreateMap<Colors, ColorDto>();
            CreateMap<ColorDto, Colors>();

            CreateMap<Cities, CityDto>();
            CreateMap<CityDto, Cities>();

            CreateMap<Provinces, ProvinceDto>().ForMember(x=> x.city, y=> y.MapFrom(t=> t.city));
            CreateMap<ProvinceDto, Provinces>();

            CreateMap<ColorProducts, ColorProductDto>()
                .ForMember(x=> x.color, y=> y.MapFrom(t=> t.color))
                .ForMember(x=> x.products, y => y.MapFrom(t=> t.products));
            CreateMap<ColorProductDto, ColorProducts>();

            CreateMap<ColorProducts, ColorProductDto>()
               .ForMember(x => x.color, y => y.MapFrom(t => t.color))
               .ForMember(x => x.products, y => y.MapFrom(t => t.products));
            CreateMap<ColorProductDto, ColorProducts>();

            CreateMap<ContactData, ContactDataDto>();
            CreateMap<ContactDataDto, ContactData>();

            CreateMap<SizeNumProduct, SizeNumProductDto>()
                .ForMember(x => x.product, y => y.MapFrom(t => t.product))
                .ForMember(x => x.size, y => y.MapFrom(t => t.size));
            CreateMap<SizeNumProductDto, SizeNumProduct>();

            CreateMap<ProductFavorite, ProductFavoriteDto>()
              .ForMember(x => x.product, y => y.MapFrom(t => t.product))
              .ForMember(x => x.shoppers, y => y.MapFrom(t => t.shoppers));
            CreateMap<ProductFavoriteDto, ProductFavorite>();

            CreateMap<ImagesProductDto, ImagesProduct>().ForMember(x => x.product, y=> y.MapFrom(t=> t.product));
            CreateMap<ImagesProduct, ImagesProductDto>();

            CreateMap<Order, OrderDto>()
             .ForMember(x => x.shopper, y => y.MapFrom(t => t.shopper));
            CreateMap<OrderDto, Order>();

            CreateMap<OrderProducts, OrderProductsDto>()
            .ForMember(x => x.product, y => y.MapFrom(t => t.product))
            .ForMember(x => x.order, y => y.MapFrom(t => t.order));
            CreateMap<OrderProductsDto, OrderProducts>();
        }
    }
}
