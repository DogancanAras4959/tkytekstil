using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;

namespace tkytekstil.ENGINE.Dtos.BrandData
{
    public class BrandDto : BaseEntityDto
    {
        public string BrandName { get; set; }
        public string BrandImage { get; set; }
        public ICollection<Products> productBrandList { get; set; }
    }
}
