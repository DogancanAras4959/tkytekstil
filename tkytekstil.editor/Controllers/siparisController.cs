using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.ENGINE.Dtos.ContactDataData;
using tkytekstil.ENGINE.Dtos.OrderProductData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.editor.Controllers
{
    public class siparisController : Controller
    {

        #region Fields

        private readonly IOrderService _orderService;
        private readonly IOrderProductsService _orderProductsService;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IViewRenderService _viewRenderService;
        private readonly IContactDataService _contactDataService;
        private readonly IShoppersService _shopperService;

        #endregion

        public siparisController(IOrderService orderService, IOrderProductsService orderProductsService, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment, IViewRenderService viewRenderService, IContactDataService contactDataService, IShoppersService shopperService)
        {
            _orderService = orderService;
            _orderProductsService = orderProductsService;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
            _viewRenderService = viewRenderService;
            _contactDataService = contactDataService;
            _shopperService = shopperService;
        }

        [HttpGet]
        public IActionResult bekleyen()
        {
            return View(_orderService.ordersByShopper());
        }

        [HttpGet]
        public IActionResult onaylı()
        {
            return View(_orderService.ordersByCompletedShopper());
        }

        [HttpGet]
        public IActionResult siparisUrunleri(int Id)
        {

            var orderProducts = _orderProductsService.orderToProducts(Id);
            return View(orderProducts);

        }

        [HttpGet]
        public async Task<IActionResult> siparisOnayla(int Id)
        {
            try
            {
                var order = _orderService.Get(Id);
                order.IsDone = true;
                _orderService.Update(order);

                List<OrderProductsDto> orderProducts = _orderProductsService.orderToProducts(order.ID);

                var bayi = _shopperService.Get(order.ShopperId);

                string pathToFile = _webHostEnvironment.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Views" + Path.DirectorySeparatorChar.ToString() + "siparis" + Path.DirectorySeparatorChar.ToString() + "order_appoinment_result.cshtml";

                string htmlBody = "";

                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }

                ContactDataDto message = new ContactDataDto();
                message.To = bayi.ShopperEmail;
                message.Email = bayi.ShopperEmail;
                message.Subject = bayi.ShopperName + " siparişi onaylandı";
                message.NameSurname = bayi.ShopperName;
                message.Body = await _viewRenderService.RenderToString($"/Views/siparis/order_appoinment_result.cshtml", orderProducts);

                await _contactDataService.sendMailContact(message);

                return RedirectToAction("onaylı", "siparis");
            }
            catch (Exception ex)
            {
                return RedirectToAction("onaylı","siparis");
            }
          

        }

        public IActionResult order_appoinment_result()
        {
            return View();
        }

    }
}
