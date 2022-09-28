using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.CORE.EmailConfig;
using tkytekstil.ENGINE.Dtos.CityData;
using tkytekstil.ENGINE.Dtos.ContactDataData;
using tkytekstil.ENGINE.Dtos.ProvinceData;
using tkytekstil.ENGINE.Dtos.ShoppersData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.editor.Controllers
{
    public class bayiController : Controller
    {
        #region Fields

        private readonly IShoppersService _shopperService;
        private readonly IProvinceService _provinceService;
        private readonly ICityService _cityService;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IViewRenderService _viewRenderService;
        private readonly IContactDataService _contactDataService;
        public bayiController(IShoppersService shoppersService, IProvinceService provinceService, ICityService cityService, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment, IViewRenderService viewRenderService, IContactDataService contactDataService)
        {
            _shopperService = shoppersService;
            _provinceService = provinceService;
            _cityService = cityService;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
            _viewRenderService = viewRenderService;
            _contactDataService = contactDataService;
        }

        #endregion

        [HttpGet]
        public IActionResult basvurular()
        {
            return View(_shopperService.GetAllAppoinments());
        }

        public async Task<IActionResult> basvuruonayla(int Id)
        {
            try
            {
                var bayi = _shopperService.Get(Id);
                bayi.IsAppliedAccount = true;
                _shopperService.Update(bayi);

                string pathToFile = _webHostEnvironment.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Views" + Path.DirectorySeparatorChar.ToString() + "bayi" + Path.DirectorySeparatorChar.ToString() + "email_appoinment_result.cshtml";

                string htmlBody = "";

                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }

                ContactDataDto message = new ContactDataDto();
                message.To = bayi.ShopperEmail;
                message.Email = bayi.ShopperEmail;
                message.Subject = bayi.ShopperName + " başvuru onay";
                message.NameSurname = bayi.ShopperName;
                message.Body = await _viewRenderService.RenderToString($"/Views/bayi/email_appoinment_result.cshtml", bayi);

                await _contactDataService.sendMailContact(message);

                return RedirectToAction("basvurular", "bayi");
            }
            catch (Exception)
            {
                return RedirectToAction("basvurular", "bayi");
            }

        }

        [HttpGet]
        public IActionResult basvurusil(int Id)
        {
            _shopperService.Delete(Id);
            return RedirectToAction("basvurular", "bayi");
        }

        [HttpGet]
        public IActionResult bayiler()
        {
            return View(_shopperService.GetAll());
        }

        [HttpGet]
        public IActionResult bayidetay(int Id)
        {
            var bayi = _shopperService.Get(Id);
            return View(bayi);
        }

        [HttpGet]
        public IActionResult bayiduzenle(int Id)
        {
            var bayi = _shopperService.Get(Id);
            var cities = _cityService.GetAll();
            ViewBag.City = new SelectList(cities, "ID", "CityName", bayi.CityId);

            return View(bayi);
        }

        [HttpPost]
        public IActionResult bayiguncelle(ShoppersDto model)
        {
            try
            {
                var bayi = _shopperService.Get(model.ID);

                var city = _cityService.Get(model.CityId);
                var province = _provinceService.Get(model.ProvinceId);

                model.ShopperCity = city.CityName;
                model.ShopperProvice = province.ProvinceName;
                model.UpdatedTime = DateTime.Now;

                _shopperService.Update(model);
                return RedirectToAction("bayiler", "bayi");
            }
            catch (Exception ex)
            {
                return RedirectToAction("bayiduzenle", "bayi", new { Id = model.ID });
            }
          
        }

        public IActionResult bayisil(int Id)
        {
            _shopperService.Delete(Id);
            return RedirectToAction("bayiler", "bayi");
        }

        public IActionResult bayidurumduzenle(int Id)
        {
            var bayi = _shopperService.Get(Id);
            if (bayi.IsActive == true)
                bayi.IsActive = false;
            else
                bayi.IsActive = true;
            _shopperService.Update(bayi);
            return RedirectToAction("bayiler", "bayi");
        }

        #region Extends

        [HttpGet]
        public JsonResult GetIlcelerBySehirID(int Id)
        {
            List<ProvinceDto> ilceler = _provinceService.provinceList(Id);
            return Json(ilceler);
        }

        public IActionResult email_appoinment_result()
        {
            return View();
        }

        #endregion
    }
}
