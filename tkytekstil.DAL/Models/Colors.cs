using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("colors")]
    public class Colors : BaseEntity, IEntity
    {
        public Colors()
        {
            colorsList = new List<ColorProducts>();
        }

        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public ICollection<ColorProducts> colorsList { get; set; }
    }
}
