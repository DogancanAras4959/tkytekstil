using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.ShoppersData;

namespace tkytekstil.ENGINE.Dtos.ProductFavoriteData
{
    public class ProductFavoriteDto : BaseEntityDto
    {
        public int ProductId { get; set; }
        public int ShopperId { get; set; }
        public ProductDto product { get; set; }
        public ShoppersDto shoppers { get; set; }
    }
}
