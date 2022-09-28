using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using tkytekstil.Core;
using tkytekstil.ENGINE.Dtos.ColorData;
using tkytekstil.ENGINE.Dtos.ColorProductData;
using tkytekstil.ENGINE.Dtos.ContactDataData;
using tkytekstil.ENGINE.Dtos.OrderData;
using tkytekstil.ENGINE.Dtos.OrderProductData;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.ProductFavoriteData;
using tkytekstil.ENGINE.Dtos.ShoppersData;
using tkytekstil.ENGINE.Interface;
using tkytekstil.Models;

namespace tkytekstil.Controllers
{
    public class urunController : Controller
    {

        #region Fields

        private readonly IProductService _productService;
        private readonly IImageProductService _imageProductService;
        private readonly ISizeNumberProductService _sizeNumberProductService;
        private readonly IColorProductService _colorProductService;
        private readonly IColorService _colorService;
        private readonly IProductFavoriteService _productFavoriteService;
        private readonly IOrderService _orderService;
        private readonly IOrderProductsService _orderProductService;
        private readonly IContactDataService _contactDataService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IViewRenderService _viewRenderService;
        private readonly ISizeNumService _sizeNumService;

        public urunController(IProductService productService, IImageProductService imageProductService, ISizeNumberProductService sizeNumberProductService, IColorProductService colorProductService, IColorService colorService, IProductFavoriteService productFavoriteService, IOrderService orderService, IOrderProductsService orderProductService, IContactDataService contactDataService, IWebHostEnvironment webHostEnvironment, IViewRenderService viewRenderService, ISizeNumService sizeNumService)
        {
            _productService = productService;
            _imageProductService = imageProductService;
            _sizeNumberProductService = sizeNumberProductService;
            _colorProductService = colorProductService;
            _colorService = colorService;
            _productFavoriteService = productFavoriteService;
            _orderService = orderService;
            _orderProductService = orderProductService;
            _contactDataService = contactDataService;
            _webHostEnvironment = webHostEnvironment;
            _viewRenderService = viewRenderService;
            _sizeNumService = sizeNumService;
        }

        #endregion

        [HttpGet("detay/{id}/{productname}", Name = "urun")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult detay(int Id, string productname)
        {
            var value = _productService.getProduct(Id);
            string name = productname;

            ShoppersDto shopper = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");
          
            if (shopper != null)
            {
                var pf = _productFavoriteService.getFavorite(value.ID, shopper.ID);

                if (pf != null)
                {
                    ViewBag.pf = pf;
                }
            }
     
            ViewBag.Images = _imageProductService.getToIntProduct(value.ID);
            ViewBag.ProductsLikesList = _productService.productListToCategoryId(value.CategoryId);
            ViewBag.SizeProductList = _sizeNumberProductService.listSizeNumProductToProduct(value.ID);

            List<ColorProductDto> colorProducts = _colorProductService.colorToProductId(value.ID);
            List<ColorDto> newColors = new List<ColorDto>();

            foreach (var item in colorProducts)
            {
                var getColor = _colorService.Get(item.ColorId);
                ColorDto color = new ColorDto()
                {
                    ColorCode = getColor.ColorCode,
                    ColorName = getColor.ColorName,
                    IsActive = true,
                    ID = getColor.ID,
                };

                newColors.Add(color);
            }

            ViewBag.ColorProductList = newColors;
           
            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = value.ProductName;
            //meta.Keywords = newsGet.Tag;
            meta.Description = value.ProductSpot;
            meta.Image = "https://uploads.ikifikir.net/tky/" + value.ProductBaseImage;
            meta.ogDescription = value.ProductSpot;
            meta.ogTitle = value.ProductName;
            meta.ogImage = "https://uploads.ikifikir.net/tky/" + value.ProductBaseImage;
            meta.Url = "https://ikifikir.net/" + Id + "/" + value.ProductName;
            ViewBag.Meta = meta;

            #endregion

            if (!string.Equals(name, productname, StringComparison.Ordinal))
            {
                return this.RedirectToRoutePermanent("detay", new { id = Id, title = name });
            }

            return View(value);
        }

        public IActionResult favorilereekle(int productId) 
        {
            var product = _productService.Get(productId);
            ShoppersDto bayi = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");

            if (bayi != null)
            {
                ProductFavoriteDto pf = new ProductFavoriteDto()
                {
                    IsActive = true,
                    ShopperId = bayi.ID,
                    ProductId = product.ID,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                };

                _productFavoriteService.Insert(pf);
                return RedirectToAction("detay", "urun", new { Id = productId, productName = product.GenerateSlug() });
            }
            else
            {
                return RedirectToAction("girisyap", "bayi");
            }
        }

        public IActionResult favorilerdencikar(int productId)
        {
            var product = _productService.Get(productId);
            ShoppersDto bayi = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");
            _productFavoriteService.deleteToFavorite(product.ID, bayi.ID);
            return RedirectToAction("detay", "urun", new { Id = productId, productName = product.GenerateSlug() });
        }

        #region Cart Checkout

        [HttpGet]
        [Route("sepet")]
        public IActionResult sepet()
        {
            var cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
            ViewBag.Cart = cart;
            //ViewBag.CartTotal = cart.Sum(x => x.products.ProductPrice * x.products.Quantity);
            return View();
        }

        [HttpGet]
        [Route("siparis")]
        public IActionResult siparis()
        {
            //ViewBag.CartTotal = cart.Sum(x => x.products.ProductPrice * x.products.Quantity);
            return View();
        }

        public async Task<IActionResult> siparistamamla()
        {
            try
            {
                var shopper = SessionExtensionMethod.GetObject<ShoppersDto>(HttpContext.Session, "account");
                var cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");

                Random r = new Random();
                int orderNo = r.Next();

                OrderDto order = new OrderDto()
                {
                    OrderNo = orderNo.ToString(),
                    ShopperId = shopper.ID,
                    IsActive = true,
                    IsDone = false,
                    Quantity = cart.Sum(x => x.Quantity),
                };

                int result = _orderService.insertOrder(order);

                if (result > 0)
                {
                    foreach (var item in cart)
                    {

                        OrderProductDto newOrderProduct = new OrderProductDto()
                        {
                            OrderId = result,
                            CreatedTime = DateTime.Now,
                            Color = item.Color,
                            Size = item.Size,
                            IsActive = true,
                            QuantityItem = item.Quantity,
                            ProductId = item.products.ID,
                        };

                        _orderProductService.Insert(newOrderProduct);
                    }

                    var callbackUrl = Url.Page("/bayi/hesabim", pageHandler: null, values: new {  }, protocol: Request.Scheme);

                    string pathToFile = _webHostEnvironment.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Views" + Path.DirectorySeparatorChar.ToString() + "urun" + Path.DirectorySeparatorChar.ToString() + "email_order_complete.cshtml";

                    string htmlBody = "";

                    using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                    {
                        htmlBody = streamReader.ReadToEnd();
                    }

                    string messageLink = $"Sipariş detayları için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>";

                    List<CartItems> newCarts = new List<CartItems>();

                    foreach (var item in cart)
                    {
                        var product = _productService.Get(item.products.ID);

                        CartItems cartNew = new CartItems
                        {
                            products = product,
                            Quantity = item.Quantity,
                            Color = item.Color,
                            Size = item.Size
                        };
                        newCarts.Add(cartNew);
                    }

                    ContactDataDto message = new ContactDataDto()
                    {
                        CompanyName = shopper.CompanyName,
                        NameSurname = shopper.ShopperName,
                        Address = shopper.ShopperAddress,
                        City = shopper.ShopperCity,
                        Province = shopper.ShopperProvice,
                        Email = shopper.ShopperEmail,
                        Subject = shopper.ShopperName + " sipariş oluşturdu.",
                        Phone = shopper.ShopperPhone,
                        To = "siparis@tkytekstil.com",
                        Content = await _viewRenderService.RenderToString($"/Views/urun/email_order_complete.cshtml", newCarts),
                    };

                    await _contactDataService.sendMailOrderComplete(message);

                    ContactDataDto messageToShopper = new ContactDataDto()
                    {
                        CompanyName = shopper.CompanyName,
                        NameSurname = shopper.ShopperName,
                        Address = shopper.ShopperAddress,
                        City = shopper.ShopperCity,
                        Province = shopper.ShopperProvice,
                        Email = shopper.ShopperEmail,
                        Subject = shopper.ShopperName + " sipariş oluşturdu.",
                        Phone = shopper.ShopperPhone,
                        To = shopper.ShopperEmail,
                        Content = await _viewRenderService.RenderToString($"/Views/urun/email_order_complete.cshtml", newCarts),
                    };

                    await _contactDataService.sendMailOrderComplete(messageToShopper);

                    HttpContext.Session.Remove("cart");
                    HttpContext.Session.Clear();
                }

                return RedirectToAction("sonuc", "urun");
            }
            catch (Exception ex)
            {
                return RedirectToAction("sonuc", "urun");
            }
           
        }

        [HttpGet]
        [Route("sonuc")]
        public IActionResult sonuc()
        {
            return View();
        }
    
        private int isHaveItem(int id)
        {
            List<CartItems> cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].products.ID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        
        public IActionResult addCart(int Id, int adet, int colorId, int sizeId)
        {

            var colorGet = _colorService.Get(colorId);
            var size = _sizeNumService.Get(sizeId);

            var isCart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");

            if (isCart == null)
            {
                List<CartItems> cart = new List<CartItems>();
                cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Size = size.SizeNumber });
                SessionExtensionMethod.SetObject(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItems> cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
                int index = isHaveItem(Id);

                if (index != -1)
                {
                    cart[index].Quantity += adet;
                }
                else
                {
                    cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Size = size.SizeNumber });
                }
                SessionExtensionMethod.SetObject(HttpContext.Session, "cart", cart);
            }
            return Json(true);
        }
      
        public IActionResult remove(int Id)
        {
            List<CartItems> cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
            int index = isHaveItem(Id);
            cart.RemoveAt(index);

            SessionExtensionMethod.SetObject(HttpContext.Session, "cart", cart);
            return RedirectToAction("sayfa", "anasayfa");
        }

        #endregion

        #region RazorExtern

        public IActionResult email_order_complete()
        {
            return View();
        }       

        #endregion

    }
}
