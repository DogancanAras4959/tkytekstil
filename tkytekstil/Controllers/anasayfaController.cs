using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.COMMON.Helpers;
using tkytekstil.Core;
using tkytekstil.ENGINE.Dtos.BrandData;
using tkytekstil.ENGINE.Dtos.CategoryData;
using tkytekstil.ENGINE.Dtos.ColorData;
using tkytekstil.ENGINE.Dtos.ColorProductData;
using tkytekstil.ENGINE.Dtos.ContactDataData;
using tkytekstil.ENGINE.Dtos.ImagesProductData;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.ProductFavoriteData;
using tkytekstil.ENGINE.Dtos.ShoppersData;
using tkytekstil.ENGINE.Dtos.SizeNumProductData;
using tkytekstil.ENGINE.Interface;
using tkytekstil.Models;

namespace tkytekstil.Controllers
{
    public class anasayfaController : Controller
    {

        #region Fields

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;
        private readonly IColorProductService _colorProductService;
        private readonly IContactDataService _contactDataService;
        private readonly IImageProductService _imageProductService;
        private readonly ISizeNumService _sizeNumService;
        private readonly IProductFavoriteService _productFavoriteService;
        private readonly ISizeNumberProductService _sizeNumberProductService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IViewRenderService _viewRenderService;

        public anasayfaController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IColorService colorService, IColorProductService colorProductService, IContactDataService contactDataService, IImageProductService imageProductService, ISizeNumService sizeNumService, IProductFavoriteService prodcutFavoriteService, ISizeNumberProductService sizeNumberProductService, IWebHostEnvironment webHostEnvironment, IViewRenderService viewRenderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _colorService = colorService;
            _colorProductService = colorProductService;
            _contactDataService = contactDataService;
            _imageProductService = imageProductService;
            _sizeNumService = sizeNumService;
            _productFavoriteService = prodcutFavoriteService;
            _sizeNumberProductService = sizeNumberProductService;
            _webHostEnvironment = webHostEnvironment;
            _viewRenderService = viewRenderService;
        }

        #endregion

        [HttpGet]
        public IActionResult sayfa()
        {
            DataPage();
            return View();
        }

        [HttpGet]
        public IActionResult urunlerimiz(int? pageNumber, int? pageSizeGet, int? categoryId, int? brandId, int? sizeId, string searchString, string renkfiltre = "", string renkFiltreClose="")
        {
            int pageSize = 0;
            List<ColorDto> colorFilter = null;
            List<ProductDto> productFilter = null;
            var colorProducts = _colorProductService.GetAll();
            var colorIsHave = SessionExtensionMethod.GetObject<List<ColorDto>>(HttpContext.Session, "filterColorSession");

            if (pageSizeGet > 0)
                pageSize = (int)pageSizeGet;    
            else
                pageSize = 30;

            int result = CalculatePageSize(pageSize);

            List<ProductDto> products = null;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                products = _productService.productListToSearch(searchString);
                TempData["keyword"] = searchString;
                TempData["count"] = products.Count;
            }

            else if(renkFiltreClose != "")
            {
                if (colorIsHave != null)
                {
                    #region Color Session

                    colorFilter = new List<ColorDto>();
                    productFilter = new List<ProductDto>();

                    foreach (var item in colorIsHave)
                    {
                        if (item.ColorName != renkFiltreClose)
                        {
                            colorFilter.Add(item);
                        }
                    }

                    List<ColorDto> newColorList = new List<ColorDto>();
                    newColorList = colorFilter;

                    foreach (var item in newColorList)
                    {
                        List<ColorProductDto> colorProductNew = _colorProductService.colorToColorId(item.ID);
                        List<ProductDto> lastList = new List<ProductDto>();
                        if (productFilter.Count > 0)
                        {
                            foreach (var item2 in colorProductNew)
                            {
                                foreach (var item3 in productFilter)
                                {
                                    if (item2.ProductId == item3.ID)
                                    {
                                        var product = _productService.getProduct(item2.ProductId);
                                        lastList.Add(product);
                                    }
                                }
                            }
                            productFilter = lastList;
                        }
                        else
                        {
                            foreach (var item4 in colorProductNew)
                            {
                                var product = _productService.getProduct(item4.ProductId);
                                productFilter.Add(product);
                            }
                        }
                    }

                    var settings = new Newtonsoft.Json.JsonSerializerSettings();
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    string data = JsonConvert.SerializeObject(newColorList, settings);
                    var deSerilizeData = JsonConvert.DeserializeObject(data).ToString();
                    var resultSession = JsonConvert.DeserializeObject<List<ColorDto>>(deSerilizeData);
                    SessionExtensionMethod.SetObject(HttpContext.Session, "filterColorSession", resultSession);

                    colorIsHave = SessionExtensionMethod.GetObject<List<ColorDto>>(HttpContext.Session, "filterColorSession");

                    #endregion

                    TempData["Count"] = productFilter.Count;
                    ViewBag.ListColorFilter = colorIsHave;
                    ViewBag.Products = PaginationList<ProductDto>.Create(productFilter, pageNumber ?? 1, pageSize);
                    FilterData();
                    return View();
                }
            }

            else if (renkfiltre != "")
            {

                if (colorIsHave != null)
                {
                    #region Color Session

                    colorFilter = new List<ColorDto>();
                    productFilter = new List<ProductDto>();

                    foreach (var item in colorIsHave)
                    {
                        colorFilter.Add(item);
                    }

                    var getColor = _colorService.getColorByName(renkfiltre);

                    colorFilter.Add(getColor);

                    List<ColorDto> newColorList = new List<ColorDto>();
                    newColorList = colorFilter;

                    foreach (var item in newColorList)
                    {
                        List<ColorProductDto> colorProductNew = _colorProductService.colorToColorId(item.ID);
                        List<ProductDto> lastList = new List<ProductDto>();
                        if (productFilter.Count > 0)
                        {
                            foreach (var item2 in colorProductNew)
                            {
                                foreach (var item3 in productFilter)
                                {
                                    if (item2.ProductId == item3.ID)
                                    {
                                        var product = _productService.getProduct(item2.ProductId);
                                        lastList.Add(product);
                                    }
                                }
                            }
                            productFilter = lastList;
                        }
                        else
                        {
                            foreach (var item4 in colorProductNew)
                            {
                                var product = _productService.getProduct(item4.ProductId);
                                productFilter.Add(product);
                            }
                        }
                    }

                    var settings = new Newtonsoft.Json.JsonSerializerSettings();
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    string data = JsonConvert.SerializeObject(newColorList, settings);
                    var deSerilizeData = JsonConvert.DeserializeObject(data).ToString();
                    var resultSession = JsonConvert.DeserializeObject<List<ColorDto>>(deSerilizeData);
                    SessionExtensionMethod.SetObject(HttpContext.Session, "filterColorSession", resultSession);

                    colorIsHave = SessionExtensionMethod.GetObject<List<ColorDto>>(HttpContext.Session, "filterColorSession");

                    #endregion

                    TempData["Count"] = productFilter.Count;
                    ViewBag.ListColorFilter = colorIsHave;
                    ViewBag.Products = PaginationList<ProductDto>.Create(productFilter, pageNumber ?? 1, pageSize);
                    FilterData();
                    return View();
                }

                else
                {
                    var listColorProduct = _colorProductService.GetAll();
                    colorFilter = new List<ColorDto>();

                    var colorGet = _colorService.getColorByName(renkfiltre);

                    colorFilter.Add(colorGet);

                    productFilter = new List<ProductDto>();

                    foreach (var item in colorFilter)
                    {
                        foreach (var item2 in listColorProduct.Where(x=> x.ColorId == item.ID))
                        {
                            var product = _productService.getProduct(item2.ProductId);
                            productFilter.Add(product);
                        }
                    }

                    var settings = new Newtonsoft.Json.JsonSerializerSettings();
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    string data = JsonConvert.SerializeObject(colorFilter, settings);
                    var deSerilizeData = JsonConvert.DeserializeObject(data).ToString();
                    var resultSession = JsonConvert.DeserializeObject<List<ColorDto>>(deSerilizeData);
                    SessionExtensionMethod.SetObject(HttpContext.Session, "filterColorSession", resultSession);

                    TempData["Count"] = productFilter.Count;
                    ViewBag.Products = PaginationList<ProductDto>.Create(productFilter, pageNumber ?? 1, pageSize);
                    FilterData();
                    return View();
                }

            }

            else if (categoryId > 0)
            {
                var category = _categoryService.Get((int)categoryId);
                products = _productService.productListToNullableCategoryId(categoryId);
                TempData["keyword"] = category.CategoryName;
                TempData["count"] = products.Count;
            }

            else if (brandId > 0)
            {
                var brand = _brandService.Get((int)brandId);
                products = _productService.productListToNullableBrandId(brandId);
                TempData["keyword"] = brand.BrandName;
                TempData["count"] = products.Count;
            }

            else if(sizeId > 0)
            {
                products = new List<ProductDto>();
                var size = _sizeNumService.Get((int)sizeId);
                List<SizeNumProductDto> sizeNumber = _sizeNumberProductService.listSizeNumProductToSize(size.ID);
     
                if (sizeNumber.Count > 0)
                {
                    foreach (var item in sizeNumber)
                    {
                        var product = _productService.getProduct(item.ProductId);
                        products.Add(product);
                    }

                    TempData["keyword"] = size.SizeNumber;
                    TempData["count"] = products.Count;

                }
                else
                {
                    products = _productService.productListToGeneral();
                }
            }

            else
            {
                products = _productService.productListToGeneral();
            }

            ViewBag.Products = PaginationList<ProductDto>.Create(products, pageNumber ?? 1, pageSize);

            List<ProductFavoriteDto> pfList = _productFavoriteService.GetAll();
            ViewBag.PfList = pfList;

            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;

            if (result == 0)
            {
                TempData["pageSizeCalculate"] = products.Count;
            }

            FilterData();
            return View();
        }

        private int CalculatePageSize(int pageSize)
        {
            List<ProductDto> products = _productService.GetAll();
            int allProductCount = products.Count;

            double countPageCount = allProductCount / pageSize;
            int result = Convert.ToInt32(countPageCount);

            return result;
        }

        [HttpGet]
        [Route("iletisim")]
        public IActionResult iletisim()
        {
            return View();
        }

        #region External
        public void DataPage()
        {
            List<CategoriesDto> categories = _categoryService.GetAll();
            ViewBag.Categories = categories;

            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;

            List<ProductDto> products = _productService.prouductsToSite(20);
            ViewBag.Products = products;

            List<ImagesProductDto> colorProducts = _imageProductService.GetAll();
            ViewBag.ImageProducts = colorProducts;

            List<CartItems> cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
            ViewBag.Cart = cart;

            List<ProductFavoriteDto> pfList = _productFavoriteService.GetAll();
            ViewBag.PfList = pfList;

            ColorManyProduct();
        }

        public void FilterData()
        {
            ViewBag.ListColor = _colorService.GetAll();
            ViewBag.ListSize = _sizeNumService.GetAll();
            ViewBag.ListBrand = _brandService.GetAll();
            ViewBag.ListCategory = _categoryService.GetAll();
            ViewBag.ListImageProducts = _imageProductService.GetAll();

            ColorManyProduct();
        }

        public void ColorManyProduct()
        {
            List<ColorProductDto> colorList = _colorProductService.GetAll();

            List<ColorProductDto> newColorList = new List<ColorProductDto>();

            foreach (var item in colorList)
            {
                var colorGet = _colorService.Get(item.ColorId);
                ColorDto newColor = new ColorDto()
                {
                    ColorName = colorGet.ColorName,
                    ColorCode = colorGet.ColorCode,
                    ID = colorGet.ID,
                };
                item.color = newColor;
                newColorList.Add(item);
            }

            ViewBag.ListColorProduct = newColorList;
        }

        [HttpPost]
        public async Task<IActionResult> iletisimformdoldur(ContactDataDto contact)
        {
            string result = "";
            try
            {

                string pathToFile = _webHostEnvironment.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Views" + Path.DirectorySeparatorChar.ToString() + "anasayfa" + Path.DirectorySeparatorChar.ToString() + "email_contact_complete.cshtml";

                string htmlBody = "";

                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }

                ContactDataDto message = new ContactDataDto();
                message.To = contact.Email;
                message.Email = contact.Email;
                message.Subject = contact.Subject;
                message.NameSurname = contact.NameSurname;
                message.Content = contact.Content;
                message.Body = await _viewRenderService.RenderToString($"/Views/anasayfa/email_contact_complete.cshtml", message);

                await _contactDataService.sendMailContact(message);

                ContactDataDto message2 = new ContactDataDto();
                message2.To = "siparis@tkytekstil.com";
                message2.Email = contact.Email;
                message2.Subject = contact.Subject;
                message2.NameSurname = contact.NameSurname;
                message2.Content = contact.Content;
                message2.Body = await _viewRenderService.RenderToString($"/Views/anasayfa/email_contact_complete.cshtml", message);

                await _contactDataService.sendMailContact(message2);

                result = "Mesajınızı aldık. En kısa sürede size dönüş sağlayacağız";
                return RedirectToAction("sonuciletisim", "anasayfa", new { result = result, type = 1 });
            }
            catch (Exception ex)
            {
                result = "İşlem sırasında bir hata meydana geldi. Lütfen tekrar deneyin";
                return RedirectToAction("sonuciletisim", "anasayfa", new { result = result, type = 0 });
            }         

        }

        [HttpGet]
        [Route("sonuciletisim")]
        public IActionResult sonuciletisim(int result, int type)
        {
            ViewBag.Result = result;
            ViewBag.Type = type;

            return View();
        }

        public IActionResult clearFilter()
        {
            HttpContext.Session.Remove("filterColorSession");

            return RedirectToAction("urunlerimiz", "anasayfa");
        }

        #endregion

        #region PartialView

        public IActionResult urunler()
        {
            return PartialView("urunler");
        }

        public IActionResult markalar()
        {
            return PartialView("markalar");
        }

        public IActionResult kategoriler()
        {
            return PartialView("kategoriler");
        }

        public IActionResult campaign()
        {
            return PartialView("campaign");
        }
        public IActionResult slider() {
            return PartialView("slider");
        }

        public IActionResult bannerDiscount()
        {
            return PartialView("bannerDiscount");
        }

        public IActionResult brands()
        {
            return PartialView("brands");
        }

        #endregion

        #region MailExtern

        public IActionResult email_contact_complete()
        {
            return View();
        }

        #endregion
    }
}
