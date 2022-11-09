using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("order")]
    public class Order : BaseEntity, IEntity
    {
        public Order()
        {
            orderProducts = new List<OrderProducts>();
        }

        [ForeignKey("shopper")]
        public int ShopperId { get; set; }
        public string OrderNo { get; set; }
        public bool IsDone { get; set; }
        public int Quantity { get; set; }
        public Shoppers shopper { get; set; }

        public List<OrderProducts> orderProducts { get; set; }
    }
}
