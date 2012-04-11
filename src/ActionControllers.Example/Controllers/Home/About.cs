using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActionControllers.Example.Controllers.Home
{
    public class About : ActionController
    {
        public ActionResult Get()
        {
            return View();
        }
    }
}