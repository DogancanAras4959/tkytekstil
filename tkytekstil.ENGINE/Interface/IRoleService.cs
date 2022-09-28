using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.RoleData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IRoleService : ICrudService<Roles,RoleDto>
    {
    }
}
