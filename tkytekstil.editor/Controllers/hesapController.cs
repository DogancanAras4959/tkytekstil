using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.COMMON.Helpers.Cyroptography;
using tkytekstil.Core;
using tkytekstil.editor.Models;
using tkytekstil.ENGINE.Dtos.RoleData;
using tkytekstil.ENGINE.Dtos.UserData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.editor.Controllers
{
    public class hesapController : Controller
    {
      
        #region Fields

        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public hesapController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        #endregion

        #region Rol İşlemleri

        [HttpGet]
        public IActionResult roller()
        {
            #region ViewBag

            ViewBag.pTitle = "Roller";
            ViewBag.pageTitle = "Hesap";
            ViewBag.Title = "Roller";

            #endregion
            return View(_roleService.GetAll());
        }

        [HttpGet]
        public IActionResult rolkayit()
        {
            #region ViewBag

            ViewBag.pTitle = "Rol Kayıt";
            ViewBag.pageTitle = "Hesap";
            ViewBag.Title = "Rol Kayıt";

            #endregion
            return View();
        }

        [HttpPost]
        public IActionResult rolekle(RoleDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _roleService.Insert(model);
                    return RedirectToAction("roller", "hesap");
                }
                else
                {
                    return View(nameof(rolkayit));
                }
            }
            catch (Exception ex)
            {
                return View(nameof(rolkayit));
            }
        }

        [HttpGet]
        public IActionResult rolduzenle(int ID)
        {

            var role = _roleService.Get(ID);

            #region ViewBag

            ViewBag.pTitle = role.roleName;
            ViewBag.pageTitle = "Rol Düzenle";
            ViewBag.Title = role.roleName;

            #endregion

            return View(role);
        }

        [HttpPost]
        public IActionResult rolguncelle(RoleDto model)
        {
            try
            {
                var rolGetir = _roleService.Get(model.ID);
                rolGetir.roleName = model.roleName;
                rolGetir.UpdatedTime = model.UpdatedTime;
                rolGetir.IsActive = model.IsActive;

                _roleService.Update(rolGetir);
                return RedirectToAction("roller", "hesap");
            }
            catch (Exception ex)
            {
                return RedirectToAction("rolduzenle", "hesap", new { ID = model.ID });
            }
        }

        #endregion

        #region Kullanıcı İşlemleri

        [HttpGet]
        public IActionResult kullanicilar()
        {
            #region ViewBag

            ViewBag.pTitle = "Kullanıcılar";
            ViewBag.pageTitle = "Hesap";
            ViewBag.Title = "Kullanıcılar";

            #endregion

            return View(_userService.GetAll());
        }

        [HttpGet]
        public IActionResult kullanicikayit()
        {
            #region ViewBag

            ViewBag.pTitle = "Kullanıcı Kayıt";
            ViewBag.pageTitle = "Hesap";
            ViewBag.Title = "Kullanıcı Kayıt";

            #endregion

            var roller = _roleService.GetAll();
            ViewBag.Roller = new SelectList(roller, "ID", "roleName");
            return View();
        }

        [HttpPost]
        public IActionResult kullaniciekle(UserDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Password = new Crypto().TryEncrypt(model.Password);
                    _userService.Insert(model);

                    return RedirectToAction("kullanicilar","hesap");
                }
                else
                {
                    return RedirectToAction("kullaniciekle", "hesap");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("kullaniciekle", "hesap");
            }

        }

        [HttpGet]
        public IActionResult kullaniciduzenle(int Id)
        {
            #region ViewBag

            ViewBag.pTitle = "Kullanıcı Güncelle";
            ViewBag.pageTitle = "Hesap";
            ViewBag.Title = "Kullanıcı Güncelle";

            #endregion

            var user = _userService.Get(Id);
            string passwordDeCrypt = new Crypto().TryDecrypt(user.Password);
            user.Password = passwordDeCrypt;

            var roles = _roleService.GetAll();
            ViewBag.Roller = new SelectList(roles, "ID", "roleName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        public IActionResult kullaniciguncelle(UserDto model)
        {
            try
            {
                model.Password = new Crypto().TryEncrypt(model.Password);

                var user = _userService.Get(model.ID);
                user.Password = model.Password;
                user.DisplayName = model.DisplayName;
                user.UpdatedTime = DateTime.Now;
                user.UserName = model.UserName;
                user.RoleId = model.RoleId;
                user.IsActive = model.IsActive;

                _userService.Update(model);
                return RedirectToAction("kullanicilar", "hesap");
            }
            catch (Exception)
            {
                return RedirectToAction("kullaniciduzenle", "hesap", new { Id = model.ID });

            }
        }

        public IActionResult kullanicisil(int Id)
        {
            _userService.Delete(Id);
            return RedirectToAction("kullanicilar","hesap");
        }

        public IActionResult kullanicidurumduzenle(int Id)
        {
            try
            {
                var user = _userService.Get(Id);

                if (user.IsActive == true)
                    user.IsActive = false;
                else
                    user.IsActive = true;

                _userService.Update(user);
                return RedirectToAction("kullanicilar", "hesap");
            }
            catch (Exception ex)
            {
                return RedirectToAction("kullanicilar", "hesap");
            }
          
        }

        #endregion

        #region Login İşlemleri

        [HttpGet]
        public IActionResult girisyap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult girisyaplogin(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                string passCrypto = new Crypto().TryEncrypt(model.password);
                bool result = _userService.login(model.username, passCrypto);

                if (result == false)
                {
                    TempData["HataMesaji"] = "Kullanıcının giriş işlemi başarısız oldu!";
                    return View("girisyap");
                }

                else
                {
                    var getUser = _userService.getUserByName(model.username);

                    if (getUser.IsActive == true)
                    {

                        var settings = new Newtonsoft.Json.JsonSerializerSettings();
                        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                        string data = JsonConvert.SerializeObject(getUser, settings);
                        var deSerilizeData = JsonConvert.DeserializeObject(data).ToString();
                        var resultConvert = JsonConvert.DeserializeObject<UserDto>(deSerilizeData);
                        SessionExtensionMethod.SetObject(HttpContext.Session, "account", resultConvert);

                        return RedirectToAction("kontrolpaneli", "anasayfa");
                    }
                    else
                    {
                        TempData["HataMesaji"] = "Kullanıcı aktif edilmemiş";
                        return View("girisyap");
                    }
                }

            }
            else
            {
                TempData["HataMesaji"] = "Kullanıcı adı ve şifrenizi girmelisiniz";
                return View("girisyap");
            }
        }

        [HttpGet("yetkisizgiris")]
        public IActionResult yetkisizgiris()
        {
            return View();
        }

        public IActionResult cikisyap()
        {
            HttpContext.Session.Remove("account");
            HttpContext.Session.Clear();
            return RedirectToAction("girisyap", "hesap");
        }

        #endregion

    }
}
