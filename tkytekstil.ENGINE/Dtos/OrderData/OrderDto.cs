using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.ShoppersData;

namespace tkytekstil.ENGINE.Dtos.OrderData
{
    public class OrderDto : BaseEntityDto
    {

        public int ShopperId { get; set; }
        public string OrderNo { get; set; }
        public bool IsDone { get; set; }
        public int Quantity { get; set; }
        public ShoppersDto shopper { get; set; }
        public List<ProductDto> productsListItem { get; set; }
    }
}
