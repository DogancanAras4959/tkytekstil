using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    public class ProductFavorite : BaseEntity, IEntity
    {
        public ProductFavorite()
        {


        }

        [ForeignKey("product")]
        public int ProductId { get; set; }

        [ForeignKey("shoppers")]
        public int ShopperId { get; set; }

        public Products product { get; set; }
        public Shoppers shoppers { get; set; }
        
    }
}
