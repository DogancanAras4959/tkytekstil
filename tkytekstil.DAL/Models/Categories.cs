using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("categories")]
    public class Categories : BaseEntity, IEntity
    {
        public Categories()
        {
            productListCategory = new List<Products>();
        }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImage { get; set; }

        public ICollection<Products> productListCategory;
    }
}
