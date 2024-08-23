using Iyzipay.Model.V2.Subscription;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
using tkytekstil.ENGINE.Dtos.SizeData;
using tkytekstil.ENGINE.Interface;
using tkytekstil.Models;
using Iyzipay;
using tkytekstil.DAL.Models;

namespace tkytekstil.Controllers
{

    [ResponseCache(CacheProfileName = "Default120")]
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
        private readonly IShoppersService _shopperservice;

        public urunController(IProductService productService, IImageProductService imageProductService, ISizeNumberProductService sizeNumberProductService, IColorProductService colorProductService, IColorService colorService, IProductFavoriteService productFavoriteService, IOrderService orderService, IOrderProductsService orderProductService, IContactDataService contactDataService, IWebHostEnvironment webHostEnvironment, IViewRenderService viewRenderService, ISizeNumService sizeNumService, IShoppersService shopperService)
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
            _shopperservice = shopperService;
        }

        #endregion

        [HttpGet("detay/{id}/{productname}", Name = "urun")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult detay(int Id, string productname)
        {
            TempData["FooterHave"] = 1;
            var value = _productService.getProduct(Id);
            string name = productname;

            var isAuthenticated = User.Identity.IsAuthenticated;
            ShoppersDto shopper = null;

            if (isAuthenticated)
            {
                shopper = _shopperservice.Get(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }

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
            ViewBag.Shopper = shopper;

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

            var isAuthenticated = User.Identity.IsAuthenticated;
            ShoppersDto bayi = null;

            if (isAuthenticated)
            {
                bayi = _shopperservice.Get(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }

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

            var isAuthenticated = User.Identity.IsAuthenticated;
            ShoppersDto bayi = null;

            if (isAuthenticated)
            {
                bayi = _shopperservice.Get(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }

            _productFavoriteService.deleteToFavorite(product.ID, bayi.ID);
            return RedirectToAction("detay", "urun", new { Id = productId, productName = product.GenerateSlug() });
        }

        #region Cart Checkout

        [HttpGet]
        [Route("sepet")]
        public IActionResult sepet()
        {
            TempData["FooterHave"] = 1;
            var cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
            ViewBag.Cart = cart;
            //ViewBag.CartTotal = cart.Sum(x => x.products.ProductPrice * x.products.Quantity);
            return View();
        }

        [HttpGet]
        [Route("siparis")]
        public IActionResult siparis()
        {
            TempData["FooterHave"] = 1;
            var isAuthenticated = User.Identity.IsAuthenticated;
            ShoppersDto bayi = null;

            if (isAuthenticated)
            {
                bayi = _shopperservice.Get(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }

            ViewBag.Shopper = bayi;

            var cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
            ViewBag.Cart = cart;

            return View();
        }

        public async Task<IActionResult> siparistamamla()
        {
            try
            {
                TempData["FooterHave"] = 1;

                var isAuthenticated = User.Identity.IsAuthenticated;
                ShoppersDto shopper = null;

                if (isAuthenticated)
                {
                    shopper = _shopperservice.Get(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                }

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

                foreach (var item in cart)
                {

                    OrderProductsDto newOrderProduct = new OrderProductsDto()
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

                if (result > 0)
                {

                    List<CartItems> newCarts = new List<CartItems>();

                    foreach (var item in cart)
                    {
                        var product = _productService.getProduct(item.products.ID);

                        CartItems cartNew = new CartItems
                        {
                            products = product,
                            Quantity = item.Quantity,
                            Color = item.Color,
                            Size = item.Size,
                            Price= item.Price * item.Quantity,               
                        };
                        newCarts.Add(cartNew);
                    }

                    Options options = new Options();
                    options.ApiKey = "xccI8zO2I0Z0hm0s3wYmcCELTK3X7Y4t"; //Iyzico Tarafından Sağlanan Api Key
                    options.SecretKey = "EyIvVToaDxVEFAGqD0TolRNJpmaOgQME"; //Iyzico Tarafından Sağlanan Secret Key
                    options.BaseUrl = "https://api.iyzipay.com";

                    int priceConverted = Convert.ToInt32(newCarts.Sum(x => x.Price));

                    //Kart Bilgilerini Dolduralım.
                    CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
                    request.Locale = Locale.TR.ToString();
                    request.ConversationId = order.ShopperId.ToString();
                    request.Price = priceConverted.ToString(); // Tutar // price ile değiştireceğiz
                    request.PaidPrice = priceConverted.ToString();
                    request.Currency = Currency.TRY.ToString();
                    request.BasketId = order.OrderNo;
                    request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
                    request.CallbackUrl = "https://tkytekstil.com/sonuc"; /// Geri Dönüş Urlsi

                    List<int> enabledInstallments = new List<int>();
                    enabledInstallments.Add(2);
                    enabledInstallments.Add(3);
                    enabledInstallments.Add(6);
                    enabledInstallments.Add(9);
                    request.EnabledInstallments = enabledInstallments;

                    //Alıcı Bilgilerini Dolduralım.
                    Buyer buyer = new Buyer();
                    buyer.Id = shopper.ID.ToString();
                    buyer.Name = shopper.ShopperName;
                    buyer.Surname = shopper.ShopperSurname;
                    buyer.GsmNumber = shopper.ShopperPhone;
                    buyer.Email = shopper.ShopperEmail;
                    buyer.IdentityNumber = "64801087004";
                    buyer.LastLoginDate = shopper.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                    buyer.RegistrationDate = shopper.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                    buyer.RegistrationAddress = shopper.ShopperCity;
                    buyer.Ip = "91.93.129.194";
                    buyer.City = shopper.ShopperCity;
                    buyer.Country = "Turkey";
                    buyer.ZipCode = "35130";
                    request.Buyer = buyer;
                    Address shippingAddress = new Address();
                    shippingAddress.ContactName = shopper.ShopperName + " " + shopper.ShopperSurname;
                    shippingAddress.City = shopper.ShopperCity;
                    shippingAddress.Country = "Turkey";
                    shippingAddress.Description = shopper.ShopperAddress;
                    shippingAddress.ZipCode = "35130";
                    request.ShippingAddress = shippingAddress;
                    Address billingAddress = new Address();
                    billingAddress.ContactName = shopper.ShopperName + " " + shopper.ShopperSurname;
                    billingAddress.City = "İstanbul";
                    billingAddress.Country = "Turkey";
                    billingAddress.Description = shopper.ShopperAddress;
                    billingAddress.ZipCode = "35130";
                    request.BillingAddress = billingAddress;

                    List<BasketItem> basketItems = new List<BasketItem>();

                    //DummyListCreateile Yeni Bir Ürün Listesi Olşuturarak, Iyzico BasketItem modeline aktarıyoruz.
                    //Buradaki Listeyi, Farklı bir View'den ürün seçimi yapılarak alabilirsiniz. !

                    //Satın alınan ürün bilgilerini dolduralım.
                    foreach (var item in newCarts)
                    {
                        BasketItem basketItem = new BasketItem();
                        basketItem.Id = item.products.ID.ToString();
                        basketItem.Name = item.products.ProductName;
                        basketItem.Category1 = item.products.categoryProduct.CategoryName;
                        basketItem.Category2 = "yok";
                        basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                        basketItem.Price = Convert.ToInt32(item.Price).ToString();
                        basketItems.Add(basketItem);
                    }

                    request.BasketItems = basketItems;
                    CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
                    ViewBag.Iyzico = checkoutFormInitialize.CheckoutFormContent;
                    ViewBag.Error = checkoutFormInitialize.ErrorMessage;
                    return View();

                }
                else
                {
                    return RedirectToAction("sonuc", "urun");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.Remove("cart");
                HttpContext.Session.Clear();

                return RedirectToAction("sonuc", "urun");
            }

        }

        [HttpGet]
        [Route("resultpay")]
        public async Task<IActionResult> resultpay(RetrieveCheckoutFormRequest model)
        {
            try
            {
                string data = "";
                Options options = new Options();
                options.ApiKey = "xccI8zO2I0Z0hm0s3wYmcCELTK3X7Y4t"; //Iyzico Tarafından Sağlanan Api Key
                options.SecretKey = "EyIvVToaDxVEFAGqD0TolRNJpmaOgQME"; //Iyzico Tarafından Sağlanan Secret Key
                options.BaseUrl = "https://api.iyzipay.com";
                data = model.Token;
                RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
                request.Token = data;
                CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);
         
                if (checkoutForm.PaymentStatus == "SUCCESS")
                {
                    #region İşlemler
                    var callbackUrl = Url.Page("/bayi/hesabim", pageHandler: null, values: new { }, protocol: Request.Scheme);

                    string pathToFile = _webHostEnvironment.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Views" + Path.DirectorySeparatorChar.ToString() + "urun" + Path.DirectorySeparatorChar.ToString() + "email_order_complete.cshtml";

                    string htmlBody = "";

                    using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                    {
                        htmlBody = streamReader.ReadToEnd();
                    }

                    string messageLink = $"Sipariş detayları için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>";

                    var isAuthenticated = User.Identity.IsAuthenticated;
                    ShoppersDto shopper = null;

                    if (isAuthenticated)
                    {
                        shopper = _shopperservice.Get(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                    }

                    var cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");

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
                    #endregion

                    HttpContext.Session.Remove("cart");
                    HttpContext.Session.Clear();

                    return RedirectToAction("sonuc", "urun", new { error = "Başarılı!" });
                }
                else
                {
                    return RedirectToAction("sonuc", "urun", new { error = "Ödeme alınamadı" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("sonuc", "urun", new { error = ex.ToString() });
            }
        }


        [HttpGet]
        [Route("sonuc")]
        public async Task<IActionResult> sonuc(RetrieveCheckoutFormRequest model)
        {
            try
            {
                string data = "";
                Options options = new Options();
                options.ApiKey = "xccI8zO2I0Z0hm0s3wYmcCELTK3X7Y4t"; //Iyzico Tarafından Sağlanan Api Key
                options.SecretKey = "EyIvVToaDxVEFAGqD0TolRNJpmaOgQME"; //Iyzico Tarafından Sağlanan Secret Key
                options.BaseUrl = "https://api.iyzipay.com";
                data = model.Token;
                RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
                request.Token = data;
                CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);

                if (checkoutForm.PaymentStatus == "SUCCESS")
                {
                    #region İşlemler
                    var callbackUrl = Url.Page("/bayi/hesabim", pageHandler: null, values: new { }, protocol: Request.Scheme);

                    string pathToFile = _webHostEnvironment.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Views" + Path.DirectorySeparatorChar.ToString() + "urun" + Path.DirectorySeparatorChar.ToString() + "email_order_complete.cshtml";

                    string htmlBody = "";

                    using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                    {
                        htmlBody = streamReader.ReadToEnd();
                    }

                    string messageLink = $"Sipariş detayları için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>";

                    var isAuthenticated = User.Identity.IsAuthenticated;
                    ShoppersDto shopper = null;

                    if (isAuthenticated)
                    {
                        shopper = _shopperservice.Get(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                    }

                    var cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");

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
                    #endregion

                    HttpContext.Session.Remove("cart");
                    HttpContext.Session.Clear();
                    ViewBag.Error = "";

                    return View();
                }
                else
                {
                    ViewBag.Error = "Sipariş alınamadı! Bir hata oluştu";

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                return View();
            }
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
        
        public IActionResult addCart(int Id, int adet, int colorId, int sizeId, int fiyat)
        {

            var colorGet = _colorService.Get(colorId);
            SizeDto size = null;

            if (sizeId > 0)
            {
                size = _sizeNumService.Get(sizeId);
            }

            var isCart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
            var products = _productService.Get(Id);

            if (isCart == null)
            {
                List<CartItems> cart = new List<CartItems>();
                if (sizeId == 0)
                {
                    cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Image = products.ProductBaseImage, Price = fiyat });
                }
                else
                {
                    cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Size = size.SizeNumber, Image = products.ProductBaseImage, Price = fiyat });
                }
            

                SessionExtensionMethod.SetObject(HttpContext.Session, "cart", cart);
            }

            else
            {
                List<CartItems> cart = SessionExtensionMethod.GetObject<List<CartItems>>(HttpContext.Session, "cart");
                int index = isHaveItem(Id);

                if (index != -1)
                {
                    if (cart[index].Color != colorGet.ColorName || cart[index].Size != size.SizeNumber)
                    {
                        if(size == null)
                        {
                            cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Image = products.ProductBaseImage, Price = fiyat });
                        }
                        cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Size = size.SizeNumber, Image = products.ProductBaseImage, Price = fiyat });
                    } 
                    else
                    {
                        cart[index].Quantity += adet;
                    }
                }
                else
                {
                    if(size != null)
                    {
                        cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Size = size.SizeNumber, Image = products.ProductBaseImage, Price = fiyat });
                    }
                    else
                    {
                        cart.Add(new CartItems { products = _productService.Get(Id), Quantity = adet, Color = colorGet.ColorName, Image = products.ProductBaseImage, Price = fiyat });
                    }
                  
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
