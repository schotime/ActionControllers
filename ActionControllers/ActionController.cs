using System.Web.Mvc;

namespace ActionControllers
{
    public class ActionController : Controller, IHaveNamingConventions
    {
        protected override void ExecuteCore()
        {
            if (!ControllerContext.IsChildAction)
            {
                TempData.Load(ControllerContext, TempDataProvider);
            }
            try
            {
                string httpMethod = ControllerContext.HttpContext.Request.HttpMethod;
                if (!ActionInvoker.InvokeAction(ControllerContext, httpMethod) && !ActionInvoker.InvokeAction(ControllerContext, "Execute"))
                {
                    HandleUnknownAction(httpMethod);
                }
            }
            finally
            {
                if (!ControllerContext.IsChildAction)
                {
                    TempData.Save(ControllerContext, TempDataProvider);
                }
            }
        }

        public INamingConventions NamingConventions { get; set; }

        public RedirectToRouteResult RedirectToGet()
        {
            return RedirectToAction(NamingConventions.BuildActionFromType(GetType()), NamingConventions.BuildControllerFromType(GetType()));
        }
    }
}