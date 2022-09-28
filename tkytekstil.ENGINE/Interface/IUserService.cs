using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.UserData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IUserService : ICrudService<Users, UserDto>
    {
        bool login(string username, string password);
        UserDto getUserByName(string userName);
    }
}
