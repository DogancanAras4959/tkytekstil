using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ProductData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IProductService : ICrudService<Products, ProductDto>
    {
        List<ProductDto> prouductsToSite(int max);
        List<ProductDto> productListToCategoryId(int Id);
        List<ProductDto> productListToNullableCategoryId(int? Id);
        List<ProductDto> productListToNullableBrandId(int? Id);
        List<ProductDto> productListToGeneral();
        List<ProductDto> productListToSearch(string keyword);
        ProductDto getProduct(int Id);
        int insertProduct(ProductDto product);
    }
}
