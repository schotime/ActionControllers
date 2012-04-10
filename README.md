# **Action Controllers**

***Getting Started***

1. Create folders in your Controllers directory. The last part of the namespace (folder name) will represent the *Controller* name (by default).

2. The name of the class is the *action* (by default).

3. The method names will match the HTTP method that was used to request the resource (similar to webapi).

4. Configure where to find your actions and how to resolve them.

##Examples

***Controller Action Setup***

*Basic Setup*

```cs
    protected void Application_Start()
    {
        ActionControllers.Setup(x => x.FindActionsFromCurrentAssembly());
    }
```

*Advanced Setup (using Structuremap as the IOC container)*

```cs
    protected void Application_Start()
    {
        ActionControllers.Setup(x => {
            x.FindActionsFromCurrentAssembly();
            x.FindActionsFromAssemblyContaining<Index>();
            x.IncludeActionsWhere(x=>x.Namespace.Contains("Controllers"));
            x.UsingNamingConventions(new DefaultNamingConventions());
            x.ResolveActionsBy(ObjectFactory.GetInstance);
        });
    }
```

***Controller Example***

```cs
    namespace Example.Controllers.Home
    {
        public class Index : ActionController
        {
            public ActionResult Get()
            {
                ViewBag.Message = "Welcome to ActionControllers";
                return View();
            }

            public ActionResult Post(string q)
            {
                return RedirectToGet();
            }
        }
    }
```


