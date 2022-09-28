using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;

namespace tkytekstil.ENGINE.Dtos.ColorData
{
    public class ColorDto : BaseEntityDto
    {
        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public ICollection<Products> colorsList { get; set; }
    }
}
