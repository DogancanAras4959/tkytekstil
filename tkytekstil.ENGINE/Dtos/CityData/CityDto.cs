using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;

namespace tkytekstil.ENGINE.Dtos.CityData
{
    public class CityDto : BaseEntityDto
    {
        public string CityName { get; set; }

        public List<Shoppers> shopppersList { get; set; }
        public List<Provinces> provinceList { get; set; }
    }
}
