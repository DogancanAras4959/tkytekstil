using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("orderProduct")]
    public class OrderProductsDto : BaseEntity, IEntity
    {
        public OrderProductsDto()
        {

        }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        
        [ForeignKey("order")]
        public int OrderId { get; set; }
             
        public int QuantityItem { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public Order order { get; set; }
        public Products product { get; set; }
    }
}
