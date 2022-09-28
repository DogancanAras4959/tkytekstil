
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
using tkytekstil.ENGINE.Dtos.ColorProductData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ColorProductService : CrudService<ColorProducts, ColorProductDto>, IColorProductService
    {
        public ColorProductService(IRepository<ColorProducts> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<ColorProductDto> colorToColorId(int id)
        {
            var entity = _repository.Where(x => x.ColorId == id).ToList();
            var entityDto = _mapper.Map<List<ColorProducts>, List<ColorProductDto>>(entity);
            return entityDto;
        }

        public List<ColorProductDto> colorToProductId(int Id)
        {
            var entity = _repository.Where(x => x.ProductId == Id).ToList();
            var entityDto = _mapper.Map<List<ColorProducts>, List<ColorProductDto>>(entity);
            return entityDto;
        }

        public ColorProductDto deleteColorFromProduct(int colorId, int productId)
        {
            var entity = _repository.Where(x => x.ProductId == productId && x.ColorId == colorId).SingleOrDefault();
            var entityDto = _mapper.Map<ColorProducts, ColorProductDto>(entity);
            return entityDto;
        }
    }
}
