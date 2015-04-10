[assembly: WebActivator.PostApplicationStartMethod(typeof(WebApplication3.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace WebApplication3.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Security.Entidade;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Security.Servico;
    using System.Web;
    using SimpleInjector.Advanced;
    using System.Collections.Generic;
    using Security.UserManager;
    using Microsoft.Owin.Security;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.RegisterPerWebRequest<IUserStore<Usuario>, UserStore>();
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>, RoleStore>();
            container.RegisterPerWebRequest<IdentityDbContext<Usuario>, DBContextUsuario>();
            container.RegisterPerWebRequest<IUserManager, UserManager>();
            container.RegisterPerWebRequest<IRoleManager, RoleManager>();
            container.RegisterPerWebRequest<IServicoUsuario, ServicoUsuario>();            
            container.RegisterPerWebRequest<IAuthenticationManager>(() => 
                AdvancedExtensions.IsVerifying(container) ? 
                    new Microsoft.Owin.OwinContext(new Dictionary<string, object>()).Authentication : 
                    HttpContext.Current.GetOwinContext().Authentication); 
        }
    }
}