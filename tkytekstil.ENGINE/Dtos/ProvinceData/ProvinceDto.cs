using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.CityData;
using tkytekstil.ENGINE.Dtos.ShoppersData;

namespace tkytekstil.ENGINE.Dtos.ProvinceData
{
    public class ProvinceDto : BaseEntityDto
    {
        public string ProvinceName { get; set; }
        public int CityId { get; set; }

        public CityDto city { get; set; }
        public List<ShoppersDto> shoppersList { get; set; }
    }
}
