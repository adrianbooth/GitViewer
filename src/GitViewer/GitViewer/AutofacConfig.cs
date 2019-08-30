using Autofac;
using Autofac.Integration.Mvc;
using GitViewer.Repositories;
using GitViewer.Services;
using System.Net.Http;
using System.Reflection;
using System.Web.Mvc;

namespace GitViewer
{
    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            // inject and re-use standard client for performance throughout app
            builder.Register(c => new HttpClient()).As<HttpClient>().SingleInstance();

            builder.RegisterType<GithubUserService>().As<IGithubUserService>();
            builder.RegisterType<GitHubAPIDataRepository>().As<IGitHubDataRepository>();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}