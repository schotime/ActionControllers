using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ActionControllers
{
    public class ActionControllerFactory : DefaultControllerFactory
    {
        private readonly Dictionary<string, Type> _actionTypes;
        private readonly INamingConventions _namingConventions;
        private readonly Func<Type, object> _createController;

        public ActionControllerFactory(Dictionary<string, Type> actionTypes, INamingConventions namingConventions, Func<Type, object> createController)
        {
            _actionTypes = actionTypes;
            _namingConventions = namingConventions;
            _createController = createController;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            string actionName = requestContext.RouteData.GetRequiredString("action");
            string key = _namingConventions.BuildKeyFromControllerAndAction(controllerName, actionName);

            ActionController actionInstance = null;

            try
            {
                Type t;
                if (_actionTypes.TryGetValue(key, out t))
                    actionInstance = _createController(t) as ActionController;
                else
                    return base.CreateController(requestContext, controllerName);
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Controller not found", ex);
            }

            if (actionInstance == null)
            {
                throw new HttpException(404, "Controller not found");
            }
            
            actionInstance.NamingConventions = _namingConventions;

            return actionInstance;
        }
    }
}