using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.UserData;

namespace tkytekstil.ENGINE.Dtos.RoleData
{
    public class RoleDto : BaseEntityDto
    {
        public string roleName { get; set; }
        public ICollection<UserDto> userDtoList { get; set; }
    }
}
