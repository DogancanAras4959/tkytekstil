using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("roles")]
    public class Roles : BaseEntity, IEntity
    {
        public Roles()
        {
            userRoles = new List<Users>();
        }
        public string roleName { get; set; }

        public ICollection<Users> userRoles { get; set; }
    }
}
