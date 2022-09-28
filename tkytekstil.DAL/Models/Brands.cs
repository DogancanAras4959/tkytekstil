using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("brands")]
    public class Brands : BaseEntity, IEntity
    {
        public Brands()
        {
            productBrandList = new List<Products>();
        }

        public string BrandName { get; set; }
        public string BrandImage { get; set; }

        public ICollection<Products> productBrandList { get; set; }
    }
}
