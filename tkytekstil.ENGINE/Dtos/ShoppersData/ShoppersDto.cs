using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.CityData;
using tkytekstil.ENGINE.Dtos.ProvinceData;

namespace tkytekstil.ENGINE.Dtos.ShoppersData
{
    public class ShoppersDto : BaseEntityDto
    {
        public string CompanyName { get; set; }
        public string ShopperUserName { get; set; }
        public string ShopperPassword { get; set; }
        public string ShopperName { get; set; }
        public string ShopperPhone { get; set; }
        public string ShopperEmail { get; set; }
        public string ShopperAddress { get; set; }
        public string ShopperCity { get; set; }
        public string ShopperProvice { get; set; }
        public string ShopperCountry { get; set; }
        public bool RememberMe { get; set; }
        public bool IsAppliedAccount { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }

        public CityDto city { get; set; }
        public ProvinceDto province { get; set; }
    }
}
