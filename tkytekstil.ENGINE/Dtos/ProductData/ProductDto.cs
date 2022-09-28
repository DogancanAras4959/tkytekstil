using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.BrandData;
using tkytekstil.ENGINE.Dtos.CategoryData;
using tkytekstil.ENGINE.Dtos.ImagesProductData;
using tkytekstil.ENGINE.Dtos.SizeData;

namespace tkytekstil.ENGINE.Dtos.ProductData
{
    public class ProductDto : BaseEntityDto
    {
        public string ProductName { get; set; }
        public string ProductSpot { get; set; }
        public string ProductBaseImage { get; set; }
        public decimal KDV { get; set; }
        public bool Vitrin { get; set; }
        public bool SortedRow { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }

        public string GenerateSlug()
        {
            string phrase = string.Format("{0}", ProductName);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public CategoriesDto categoryProduct { get; set; }
        public BrandDto brandProduct { get; set; }
        public SizeDto sizeProduct { get; set; }

        public ICollection<ImagesProductDto> imagesProductList { get; set; }
    }
}
