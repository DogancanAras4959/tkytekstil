using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.RoleData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class RoleService : CrudService<Roles, RoleDto>, IRoleService
    {
        public RoleService(IRepository<Roles> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
