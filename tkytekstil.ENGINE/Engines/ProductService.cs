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
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ProductService : CrudService<Products, ProductDto>, IProductService
    {
        public ProductService(IRepository<Products> repositoy, IMapper mapper) : base(repositoy, mapper)
        {

        }

        public ProductDto getProduct(int Id)
        {
            var entity = _repository.Where(x => x.Id == Id && x.IsActive == true).Include("brandProduct").Include("categoryProduct").SingleOrDefault();
            var entityDto = _mapper.Map<Products, ProductDto>(entity);
            return entityDto;
        }

        public int insertProduct(ProductDto product)
        {
            var entity = _mapper.Map<ProductDto, Products>(product);
            _repository.Add(entity);
            _repository.Save();
            product.ID = entity.Id;
            return product.ID;
        }

        public List<ProductDto> productListToCategoryId(int Id)
        {
            var entities = _repository.Where(x => x.IsActive == true && x.CategoryId == Id).Include("brandProduct").Include("categoryProduct").Take(4).OrderByDescending(x=> x.CreatedTime).ToList();
            var entityDtos = _mapper.Map<IList<Products>, List<ProductDto>>(entities);
            return entityDtos;
        }

        public List<ProductDto> productListToGeneral()
        {
            var entities = _repository.Where(x => x.IsActive == true).Include("brandProduct").Include("categoryProduct").ToList();
            var entityDtos = _mapper.Map<IList<Products>, List<ProductDto>>(entities);
            return entityDtos;
        }

        public List<ProductDto> productListToNullableBrandId(int? Id)
        {
            var entities = _repository.Where(x => x.IsActive == true && x.BrandId == Id).Include("brandProduct").Include("categoryProduct").OrderByDescending(x => x.CreatedTime).ToList();
            var entityDtos = _mapper.Map<IList<Products>, List<ProductDto>>(entities);
            return entityDtos;
        }

        public List<ProductDto> productListToNullableCategoryId(int? Id)
        {
            var entities = _repository.Where(x => x.IsActive == true && x.CategoryId == Id).Include("brandProduct").Include("categoryProduct").OrderByDescending(x => x.CreatedTime).ToList();
            var entityDtos = _mapper.Map<IList<Products>, List<ProductDto>>(entities);
            return entityDtos;
        }

        public List<ProductDto> productListToSearch(string keyword)
        {
            var entities = _repository.Where(x => x.IsActive == true && x.ProductName!.Contains(keyword)).Include("brandProduct").Include("categoryProduct").ToList();
            var entityDtos = _mapper.Map<IList<Products>, List<ProductDto>>(entities);
            return entityDtos;
        }

        public List<ProductDto> prouductsToSite(int max)
        {
            var entities = _repository.Where(x => x.IsActive == true && x.Vitrin == true).Include("brandProduct").Include("categoryProduct").ToList();
            var entityDtos = _mapper.Map<IList<Products>, List<ProductDto>>(entities);
            return entityDtos;

        }
    }
}
