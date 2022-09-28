using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    public class Provinces : BaseEntity, IEntity
    {
        public Provinces()
        {
            shopperList = new List<Shoppers>();

        }

        public string ProvinceName { get; set; }
    
        [ForeignKey("city")]
        public int CityId { get; set; }

        public Cities city { get; set; }
        public ICollection<Shoppers> shopperList { get; set; }

    }
}
