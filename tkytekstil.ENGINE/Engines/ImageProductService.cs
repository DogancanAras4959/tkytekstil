using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ImagesProductData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ImageProductService : CrudService<ImagesProduct, ImagesProductDto>, IImageProductService
    {
        public ImageProductService(IRepository<ImagesProduct> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<ImagesProductDto> getToIntProduct(int Id)
        {
            var entities = _repository.Where(x => x.ProductId == Id).ToList();
            var entityDtos = _mapper.Map<IList<ImagesProduct>, List<ImagesProductDto>>(entities);
            return entityDtos;
        }

        public bool imageInsertInProduct(List<ImagesProductDto> images)
        {
            foreach (var item in images)
            {
                var entity = _mapper.Map<ImagesProductDto, ImagesProduct>(item);
                _repository.Add(entity);
                _repository.Save();
                item.ID = entity.Id;
                return true;
            }
            return true;
        }
    }
}
