using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ProductData;

namespace tkytekstil.ENGINE.Dtos.ImagesProductData
{
    public class ImagesProductDto : BaseEntityDto
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
        public ProductDto product { get; set; }
    }
}
