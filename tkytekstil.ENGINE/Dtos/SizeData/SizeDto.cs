using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;

namespace tkytekstil.ENGINE.Dtos.SizeData
{
    public class SizeDto : BaseEntityDto
    {
        public string SizeNumber { get; set; }

        public ICollection<Products> sizeProductList { get; set; }
    }
}
