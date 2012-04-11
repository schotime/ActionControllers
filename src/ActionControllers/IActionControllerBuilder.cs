using System;

namespace ActionControllers
{
    public interface IActionControllerBuilder
    {
        void UsingNamingConventions(INamingConventions namingConventions);
        void FindActionsFromCurrentAssembly();
        void FindActionsFromAssemblyContaining<T>();
        void IncludeActionsWhere(Func<Type, bool> predicate);
        void ResolveActionsBy(Func<Type, object> actionResolver);
    }
}