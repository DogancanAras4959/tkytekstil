using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.OrderData;
using tkytekstil.ENGINE.Dtos.ProductData;

namespace tkytekstil.ENGINE.Dtos.OrderProductData
{
    public class OrderProductsDto : BaseEntityDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int QuantityItem { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public OrderDto order { get; set; }
        public ProductDto product { get; set; }
    }
}
