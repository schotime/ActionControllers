using System;

namespace ActionControllers
{
    public class DefaultNamingConventions : INamingConventions
    {
        public string BuildKeyFromType(Type type)
        {
            return BuildControllerFromType(type).ToLowerInvariant() + "." + BuildActionFromType(type).ToLowerInvariant() + ".action";
        }

        public string BuildKeyFromControllerAndAction(string controllerName, string actionName)
        {
            return controllerName.ToLowerInvariant() + "." + actionName.ToLowerInvariant() + ".action";
        }

        public string BuildControllerFromType(Type type)
        {
            int indexOfControllers = type.Namespace.LastIndexOf("Controllers.");
            var key = type.Namespace.Substring(indexOfControllers + 12);
            return key;
        }

        public string BuildActionFromType(Type type)
        {
            return type.Name;
        }
    }
}