using DatacenterMap.Infra;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Injection;

namespace DatacenterMap.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ErrosGlobaisFilterAttribute());

            // Protect all Controllers and Actions
            config.Filters.Add(new BasicAuthorization());

            // Enable Cors
            // Install-Package Microsoft.AspNet.WebApi.Cors
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var connectionString = ConfigurationManager.ConnectionStrings["DatacenterMap"].ConnectionString;

            var container = new UnityContainer();
            container.RegisterType<IDatacenterMapContext, DatacenterMapContext>(new InjectionConstructor("name=DatacenterMap"));
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
