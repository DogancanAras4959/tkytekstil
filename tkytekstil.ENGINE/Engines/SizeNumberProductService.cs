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
using tkytekstil.ENGINE.Dtos.SizeNumProductData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class SizeNumberProductService : CrudService<SizeNumProduct, SizeNumProductDto>, ISizeNumberProductService
    {
        public SizeNumberProductService(IRepository<SizeNumProduct> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public SizeNumProductDto deleteSizeFromProduct(int sizeId, int productId)
        {
            var entity = _repository.Where(x => x.ProductId == productId && x.SizeId == sizeId).SingleOrDefault();
            var entityDto = _mapper.Map<SizeNumProduct, SizeNumProductDto>(entity);
            return entityDto;
        }

        public List<SizeNumProductDto> listSizeNumProductToProduct(int Id)
        {
            var entity = _repository.Where(x => x.ProductId == Id).Include("size").ToList();
            var entityDto = _mapper.Map<List<SizeNumProduct>, List<SizeNumProductDto>>(entity);
            return entityDto;
        }

        public List<SizeNumProductDto> listSizeNumProductToSize(int? Id)
        {
            var entity = _repository.Where(x => x.SizeId == Id).Include("size").ToList();
            var entityDto = _mapper.Map<List<SizeNumProduct>, List<SizeNumProductDto>>(entity);
            return entityDto;
        }
    }
}
