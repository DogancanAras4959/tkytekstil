using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Core;

namespace tkytekstil.DAL.Models
{
    [Table("products")]
    public class Products : BaseEntity, IEntity
    {
        public Products()
        {
            productList = new List<ColorProducts>();
            imagesProductList = new List<ImagesProduct>();
            sizeNumProduct = new List<SizeNumProduct>();
            productFavoriteList = new List<ProductFavorite>();
            orderProducts = new List<OrderProducts>();
        }

        public string ProductName { get; set; }
        public string ProductSpot { get; set; }
        public string ProductBaseImage { get; set; }
        public decimal KDV { get; set; }
        public bool sortedRow { get; set; }
        public bool Vitrin { get; set; }
        public int Quantity { get; set; }
        public bool ChooseSizeIsHave { get; set; }


        [ForeignKey("categoryProduct")]
        public int CategoryId { get; set; }

        [ForeignKey("brandProduct")]
        public int BrandId { get; set; }

        public Categories categoryProduct { get; set; }
        public Brands brandProduct { get; set; }
        public ICollection<ColorProducts> productList { get; set; }
        public ICollection<ImagesProduct> imagesProductList { get; set; }
        public ICollection<SizeNumProduct> sizeNumProduct { get; set; }
        public ICollection<ProductFavorite> productFavoriteList { get; set; }
        public ICollection<OrderProducts> orderProducts { get; set; }
    }
}
