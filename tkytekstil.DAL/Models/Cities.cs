using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    public class Cities : BaseEntity, IEntity
    {
        public Cities()
        {
            provinceList = new List<Provinces>();
            shopperList = new List<Shoppers>();
        }

        public string CityName { get; set; }
        
        public ICollection<Provinces> provinceList { get; set; }
        public ICollection<Shoppers> shopperList { get; set; }
    }
}
