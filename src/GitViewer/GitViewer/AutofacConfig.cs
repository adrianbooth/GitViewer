using Autofac;
using Autofac.Integration.Mvc;
using GitViewer.Domain.Logging;
using GitViewer.Repositories;
using GitViewer.Repositories.Clients;
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
            builder.Register(c =>
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Gitviewer-api-request");
                return httpClient;
            }).As<HttpClient>().SingleInstance();

            builder.RegisterType<GithubUserService>().As<IGithubUserService>();
            builder.RegisterType<GitHubAPIDataRepository>().As<IGitHubDataRepository>();
            builder.RegisterType<SimpleLogger>().As<ILogger>();
            builder.RegisterType<BasicHttpClient>().As<IHttpClient>();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}