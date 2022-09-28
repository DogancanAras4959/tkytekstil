using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ProductData;

namespace tkytekstil.ENGINE.Dtos.CategoryData
{
    public class CategoriesDto : BaseEntityDto
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImage { get; set; }
        public ICollection<ProductDto> productListCategory { get; set; }
    }
}
