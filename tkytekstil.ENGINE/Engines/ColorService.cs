using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ColorData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ColorService : CrudService<Colors, ColorDto>, IColorService
    {
        public ColorService(IRepository<Colors> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public ColorDto getColorByName(string name)
        {
            var entity = _repository.Where(x => x.ColorName == name).SingleOrDefault();
            var entityDto = _mapper.Map<Colors, ColorDto>(entity);
            return entityDto;
        }
    }
}
