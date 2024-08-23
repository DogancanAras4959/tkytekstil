using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("Shoppers")]
    public class Shoppers : BaseEntity, IEntity
    {
        public Shoppers()
        {
            productFavoriteList = new List<ProductFavorite>();
            orders = new List<Order>();
        }

        public string CompanyName { get; set; }
        public string ShopperUserName { get; set; }
        public string ShopperPassword { get; set; }
        public string ShopperName { get; set; }
        public string ShopperSurname { get; set; }
        public string ShopperIdentityNumber { get; set; }
        public string ShopperPhone { get; set; }
        public string ShopperEmail { get; set; }
        public string ShopperAddress { get; set; }
        public string ShopperCity { get; set; }
        public string ShopperProvice { get; set; }
        public string ShopperCountry { get; set; }

        public bool IsAppliedAccount { get; set; }

        [ForeignKey("cityShopper")]
        public int CityId { get; set; }

        [ForeignKey("provinceShopper")]
        public int ProvinceId { get; set; }

        public Cities cityShopper { get; set; }
        public Provinces provinceShopper { get; set; }
        public ICollection<ProductFavorite> productFavoriteList { get; set; }
        public ICollection<Order> orders { get; set; }
    }
}
