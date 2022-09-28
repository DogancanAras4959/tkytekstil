using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("colorProducts")]
    public class ColorProducts : BaseEntity, IEntity
    {
        public ColorProducts()
        {

        }

        [ForeignKey("color")]
        public int ColorId { get; set; }
        
        [ForeignKey("products")]
        public int ProductId { get; set; }

        public Products products { get; set; }
        public Colors color { get; set; }

    }
}
