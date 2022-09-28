using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.COMMON.Helpers.Cyroptography;
using tkytekstil.Core;
using tkytekstil.CORE.EmailConfig;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Dtos.BrandData;
using tkytekstil.ENGINE.Dtos.ColorData;
using tkytekstil.ENGINE.Dtos.ColorProductData;
using tkytekstil.ENGINE.Dtos.ContactDataData;
using tkytekstil.ENGINE.Dtos.ImagesProductData;
using tkytekstil.ENGINE.Dtos.OrderData;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.ProductFavoriteData;
using tkytekstil.ENGINE.Dtos.ProvinceData;
using tkytekstil.ENGINE.Dtos.ShoppersData;
using tkytekstil.ENGINE.Engines;
using tkytekstil.ENGINE.Interface;
using tkytekstil.Models;

namespace tkytekstil.Controllers
{
    public class bayiController : Controller
    {

        #region fields

        private readonly IProvinceService _provinceService;
        private readonly IShoppersService _shopperService;
        private readonly ICityService _cityService;
        private readonly IProductService _productService;
        private readonly IImageProductService _imageProductService;
        private readonly IProductFavoriteService _productFavoriteService;
        private readonly IColorService _colorService;
        private readonly IColorProductService _colorProductService;
        private readonly IOrderService _orderService;
        private readonly IOrderProductsService _orderProductService;
        private readonly IContactDataService _contactDataService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IViewRenderService _viewRenderService;
        private readonly IBrandService _brandService;
        public bayiController(IProvinceService provinceService, IShoppersService shopperService, ICityService cityService, IProductService productService, IProductFavoriteService productFavoriteService, IImageProductService imageProductService, IColorService colorService, IColorProductService colorProductService, IOrderService orderService, IOrderProductsService orderProductService, IContactDataService contactDataService, IWebHostEnvironment webHostEnvironment, IViewRenderService viewRenderService, IBrandService brandService)
        {
            _provinceService = provinceService;
            _shopperService = shopperService;
            _cityService = cityService;
            _productService = productService;
            _productFavoriteService = productFavoriteService;
            _imageProductService = imageProductService;
            _colorService = colorService;
            _colorProductService = colorProductService;
            _orderService = orderService;
            _orderProductService = orderProductService;
            _contactDataService = contactDataService;
            _webHostEnvironment = webHostEnvironment;
            _viewRenderService = viewRenderService;
            _brandService = brandService;
        }

        #endregion

        [Route("hesabim")]
        public IActionResult hesabim()
        {
            ShoppersDto shopper = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");
            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;
            if (shopper != null)
            {
                List<OrderDto> orders = _orderService.ordersToShopper(shopper.ID);
                ViewBag.Orders = orders;
                return View();
            }
            else
            {
                return RedirectToAction("girisyap", "bayi");
            }
        }

        [Route("basvuruyap")]
        public IActionResult basvuruyap()
        {
            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;
            var cities = _cityService.GetAll();
            ViewBag.City = new SelectList(cities, "ID", "CityName");
            return View();
        }

        [Route("siparislerim")]
        public IActionResult siparislerim()
        {
            ShoppersDto shopper = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");
            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;
            if (shopper != null)
            {
                List<OrderDto> orders = _orderService.ordersToShopper(shopper.ID);
                ViewBag.Orders = orders;
                return View();
            }
            else
            {
                return RedirectToAction("girisyap", "bayi");
            }
        }

        [Route("urunlerim")]
        public IActionResult urunlerim(int Id)
        {
            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;
            if (Id == 0)
            {
                return RedirectToAction("siparislerim", "bayi");
            }
            else
            {
                ShoppersDto shopper = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");

                if (shopper != null)
                {
                    var order = _orderService.Get(Id);
                    var productsToOrder = _orderProductService.orderToProducts(Id);
                    ViewBag.OrdersToProduct = productsToOrder;
                    return View(order);
                }
                else
                {
                    return RedirectToAction("girisyap", "bayi");
                }
            }

        }

        [Route("favorilerim")]
        public IActionResult favorilerim()
        {
            ShoppersDto shopper = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");
            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;
            if (shopper != null)
            {
                List<ImagesProductDto> colorProducts = _imageProductService.GetAll();
                ViewBag.ImageProducts = colorProducts;

                List<CartItems> cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
                ViewBag.Cart = cart;

                List<ProductFavoriteDto> pfList = _productFavoriteService.getProductFavoriteByIncludes();
                ViewBag.PfList = pfList;

                ColorManyProduct();

                return View();
            }
            else
            {
                return RedirectToAction("girisyap", "bayi");
            }

        }

        #region Login

        [HttpGet]
        [Route("girisyap")]
        public IActionResult girisyap()
        {
            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;
            return View();
        }

        [HttpPost]
        public IActionResult oturumac(ShoppersDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string passCrypto = new Crypto().TryEncrypt(model.ShopperPassword);
                    bool result = _shopperService.logOn(model.ShopperUserName, passCrypto);

                    if (result != false)
                    {
                        var geShopper = _shopperService.getShopper(model.ShopperUserName);

                        var settings = new Newtonsoft.Json.JsonSerializerSettings();
                        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                        string data = JsonConvert.SerializeObject(geShopper, settings);
                        var deSerilizeData = JsonConvert.DeserializeObject(data).ToString();
                        var resultConvert = JsonConvert.DeserializeObject<ShoppersDto>(deSerilizeData);
                        SessionExtensionMethod.SetObject(HttpContext.Session, "account", resultConvert);

                        return RedirectToAction("hesabim", "bayi");
                    }
                    else
                    {
                        return RedirectToAction("sayfa", "anasayfa");
                    }
                }
                else
                {
                    return RedirectToAction("sayfa", "anasayfa");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("sayfa", "anasayfa");
            }
        }

        #endregion

        #region Register

        [HttpPost]
        public async Task<IActionResult> kayityap(ShoppersDto shopper)
        {
            string result = "";
            try
            {
                shopper.CompanyName = shopper.CompanyName;
                shopper.ShopperUserName = shopper.ShopperUserName;
                shopper.ShopperPassword = new Crypto().TryEncrypt(shopper.ShopperPassword);
                shopper.ShopperCity = shopper.ShopperCity;
                shopper.ShopperProvice = shopper.ShopperProvice;
                shopper.IsAppliedAccount = false;
                shopper.IsActive = false;
                shopper.ShopperCountry = "Türkiye";
                shopper.CityId = 1;
                shopper.ProvinceId = 3;
                _shopperService.Insert(shopper);

                ContactDataDto message = new ContactDataDto()
                {
                    CompanyName = shopper.CompanyName,
                    NameSurname = shopper.ShopperName,
                    Address = shopper.ShopperAddress,
                    City = shopper.ShopperCity,
                    Province = shopper.ShopperProvice,
                    Email = shopper.ShopperEmail,
                    Subject = shopper.ShopperName +" müşteri kaydı gerçekleştirdi",
                    Phone = shopper.ShopperPhone,
                    To = "siparis@tkytekstil.com",
                    Body = await _viewRenderService.RenderToString($"/Views/bayi/email_appoinment_register.cshtml", shopper),
                };

                await _contactDataService.sendMailContact(message);

                ContactDataDto message2 = new ContactDataDto()
                {
                    CompanyName = shopper.CompanyName,
                    NameSurname = shopper.ShopperName,
                    Address = shopper.ShopperAddress,
                    City = shopper.ShopperCity,
                    Province = shopper.ShopperProvice,
                    Email = shopper.ShopperEmail,
                    Subject = "Müşteri Kaydı",
                    Phone = shopper.ShopperPhone,
                    To = shopper.ShopperEmail,
                    Body = await _viewRenderService.RenderToString($"/Views/bayi/email_appoinment.cshtml", shopper),
                };

                await _contactDataService.sendMailContact(message2);

                result = "Kayıt işleminiz başarılı. En kısa sürede sizinle iletişime geçip hesabınız aktifleştireceğiz";

                return RedirectToAction("kayitsonuc", "bayi", new { result = result, type = 1 });

            }
            catch (Exception ex)
            {
                result = "Kayıt işleminiz sırasında bir sorun oluştu lütfen tekrar deneyiniz veya bizimle iletişime geçin";
                return RedirectToAction("kayitsonuc", "bayi", new { result = result, type = 0 }); ;

            }
        }

        public IActionResult kayitsonuc(string result, int type)
        {
            ViewBag.Type = type;
            ViewBag.Result = result; List<BrandDto> brands = _brandService.GetAll();
            ViewBag.ListBrand = brands;
            return View();
        }

        #endregion

        public IActionResult cikisyap()
        {
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Clear();
            return RedirectToAction("sayfa", "anasayfa");
        }

        #region Extends

        [HttpGet]
        public JsonResult GetIlcelerBySehirID(int Id)
        {
            List<ProvinceDto> ilceler = _provinceService.provinceList(Id);
            return Json(ilceler);
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

        public IActionResult email_appoinment()
        {
            return View();
        }

        #endregion

    }
}
