﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.RoleData;

namespace tkytekstil.ENGINE.Dtos.UserData
{
    public class UserDto : BaseEntityDto
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public RoleDto role { get; set; }
    }
}
