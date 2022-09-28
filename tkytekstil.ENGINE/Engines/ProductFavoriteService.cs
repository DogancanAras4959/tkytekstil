using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ProductFavoriteData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ProductFavoriteService : CrudService<ProductFavorite, ProductFavoriteDto>, IProductFavoriteService
    {
        public ProductFavoriteService(IRepository<ProductFavorite> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public void deleteToFavorite(int productId, int shopperId)
        {
            var getPf = _repository.Where(x => x.ProductId == productId && x.ShopperId == shopperId).SingleOrDefault();
            _repository.Delete(getPf);
            _repository.Save();
        }

        public ProductFavoriteDto getFavorite(int productId, int shopperId)
        {
            var getPf = _repository.Where(x => x.ProductId == productId && x.ShopperId == shopperId).SingleOrDefault();
            var entityDto = _mapper.Map<ProductFavorite, ProductFavoriteDto>(getPf);
            return entityDto;
        }

        public List<ProductFavoriteDto> getProductFavoriteByIncludes()
        {
            var getProductFavorite = _repository.Where(x => x.Id > 0).Include("product").Include("shoppers").ToList();
            var entityDto = _mapper.Map<List<ProductFavorite>, List<ProductFavoriteDto>>(getProductFavorite);
            return entityDto;
        }
    }
}
