using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.SizeData;

namespace tkytekstil.ENGINE.Dtos.SizeNumProductData
{
    public class SizeNumProductDto : BaseEntityDto
    {
        public int SizeId { get; set; }
        public int ProductId { get; set; }

        public SizeDto size { get; set; }
        public ProductDto product { get; set; }
    }
}
