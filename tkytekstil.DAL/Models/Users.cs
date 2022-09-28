using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("users")]
    public class Users : BaseEntity, IEntity
    {
        public Users()
        {

        }

        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [ForeignKey("role")]
        public int RoleId { get; set; }
        public Roles role { get; set; }

    }
}
