using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("size")]
    public class SizeNum : BaseEntity, IEntity
    {
        public SizeNum()
        {
            sizeNumProductList = new List<SizeNumProduct>();
        }

        public string SizeNumber { get; set; }

        public ICollection<SizeNumProduct> sizeNumProductList { get; set; }
    }
}
