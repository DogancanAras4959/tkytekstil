using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ColorData;
using tkytekstil.ENGINE.Dtos.ProductData;

namespace tkytekstil.ENGINE.Dtos.ColorProductData
{
    public class ColorProductDto : BaseEntityDto
    {

        public int ColorId { get; set; }
        public int ProductId { get; set; }

        public ProductDto products { get; set; }
        public ColorDto color { get; set; }
    }
}
