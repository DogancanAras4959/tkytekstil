using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ShoppersData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ShopperService : CrudService<Shoppers, ShoppersDto>, IShoppersService
    {
        public ShopperService(IRepository<Shoppers> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<ShoppersDto> GetAllAppoinments()
        {
            var entities = _repository.Where(x => x.IsAppliedAccount == false).ToList();
            var entityDtos = _mapper.Map<IList<Shoppers>, List<ShoppersDto>>(entities);
            return entityDtos;
        }

        public override List<ShoppersDto> GetAll()
        {
            var entities = _repository.Where(x=> x.IsAppliedAccount == true).ToList();
            var entityDtos = _mapper.Map<IList<Shoppers>, List<ShoppersDto>>(entities);
            return entityDtos;
        }

        public bool logOn(string username, string password)
        {
            var entites = _repository.Where(x => x.ShopperPassword == password && x.ShopperUserName == username).SingleOrDefault();
            var entityDto = _mapper.Map<Shoppers, ShoppersDto>(entites);

            if (entityDto != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShoppersDto getShopper(string username)
        {
            var entities = _repository.Where(x => x.ShopperUserName == username).SingleOrDefault();
            var entityDto = _mapper.Map<Shoppers, ShoppersDto>(entities);
            return entityDto;
        }
    }
}
