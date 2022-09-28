using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    public class ImagesProduct : BaseEntity
    {
        public ImagesProduct()
        {

        }

        public string Image { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }

        public Products product { get; set; }

    }
}
