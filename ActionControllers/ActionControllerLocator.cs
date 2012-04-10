using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace ActionControllers
{
    public class ControllerActionLocator
    {
        private INamingConventions namingConventions;
        private List<Type> types = new List<Type>();
        private List<Func<Type, bool>> predicates = new List<Func<Type, bool>>();
        private List<Assembly> assemblies = new List<Assembly>();

        public INamingConventions NamingConventions
        {
            get { return namingConventions; }
        }

        public ControllerActionLocator(INamingConventions namingConventions)
        {
            this.namingConventions = namingConventions;
        }

        public ControllerActionLocator FindActionsFromAssemblyContaining(Type type)
        {
            if (!assemblies.Contains(type.Assembly))
            {
                FindActionsFromAssembly(type.Assembly);
                assemblies.Add(type.Assembly);
            }

            return this;
        }

        public ControllerActionLocator FindActionsFromAssembly(Assembly assembly)
        {
            if (!assemblies.Contains(assembly))
            {
                FindActionsFrom(assembly.GetExportedTypes());
                assemblies.Add(assembly);
            }

            return this;
        }

        public ControllerActionLocator FindActionsFrom(IEnumerable<Type> types)
        {
            this.types.AddRange(types);
            return this;
        }

        public ControllerActionLocator Where(Func<Type, bool> predicate)
        {
            predicates.Add(predicate);
            return this;
        }

        public Dictionary<string, Type> Build()
        {
            var actionTypes = (from type in types
                               where type.IsPublic
                               where type != typeof (ActionController)
                               where typeof (ActionController).IsAssignableFrom(type)
                               where !type.IsAbstract
                               where !type.IsInterface
                               select type);

            var located = actionTypes
                .Where(x => predicates.All(func => func(x)))
                .Select(x => new { Type = x, Name = namingConventions.BuildKeyFromType(x)}).ToDictionary(x => x.Name, x => x.Type);
            
            return located;
        }
    }
}