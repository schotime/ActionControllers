using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ActionControllers.Example.Controllers.Account
{
    public class LogOff : ActionController
    {
        //
        // GET: /Account/LogOff

        public ActionResult Get()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}