using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.editor.Core;

namespace tkytekstil.editor.Controllers
{
    public class anasayfaController : Controller
    {

        [CheckLogin]
        public IActionResult kontrolpaneli()
        {
            
            #region ViewBag

            ViewBag.pTitle = "Kontrol Paneli";
            ViewBag.pageTitle = "Ana Sayfa";
            ViewBag.Title = "Kontrol Paneli";

            #endregion

            return View();

        }


    }
}
