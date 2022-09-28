using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ShoppersData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IShoppersService : ICrudService<Shoppers, ShoppersDto>
    {
        List<ShoppersDto> GetAllAppoinments();
        ShoppersDto getShopper(string username);
        bool logOn(string username, string password);
    }
}
