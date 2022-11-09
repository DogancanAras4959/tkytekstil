using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ProductData;

namespace tkytekstil.Models
{
    public class CartItems
    {
        public ProductDto products { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Image { get; set; }
    }
}
