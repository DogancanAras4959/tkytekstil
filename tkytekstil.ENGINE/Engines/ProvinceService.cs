using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ProvinceData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ProvinceService : CrudService<Provinces, ProvinceDto>, IProvinceService
    {
        public ProvinceService(IRepository<Provinces> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<ProvinceDto> provinceList(int Id)
        {
            var entities = _repository.Where(x => x.CityId == Id).ToList();
            var entityDtos = _mapper.Map<IList<Provinces>, List<ProvinceDto>>(entities);
            return entityDtos;
        }
    }
}
