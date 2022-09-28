using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tkytekstil.editor.Core
{
    public class CheckLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("account") == null)
            {
                filterContext.Result = new RedirectResult("/hesap/girisyap");
            }
            //base.OnActionExecuted(filterContext);
        }
    }
}
