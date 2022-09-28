using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ProductFavoriteData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IProductFavoriteService : ICrudService<ProductFavorite, ProductFavoriteDto>
    {
        ProductFavoriteDto getFavorite(int productId, int shopperId);
        List<ProductFavoriteDto> getProductFavoriteByIncludes();
        void deleteToFavorite(int productId, int shopperId);
    }
}
