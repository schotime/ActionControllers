using System;
using System.Web.Mvc;

namespace ActionControllers
{
    public class ActionControllerSetup
    {
        public static void Setup(Action<IActionControllerBuilder> action)
        {
            var builder = new ActionControllerBuilder();
            action(builder);
            var actionTypes = builder.Build();
            ControllerBuilder.Current.SetControllerFactory(new ActionControllerFactory(actionTypes, builder.NamingConventions, builder.Resolver));
        }
    }
}