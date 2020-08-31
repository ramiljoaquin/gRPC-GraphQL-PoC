using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AccountService.Commands;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AccountService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
              .UseServiceProviderFactory(new AutofacServiceProviderFactory())
              .ConfigureContainer<ContainerBuilder>(container => {
                  container.RegisterType<ProfileCommandBus>().As<ICommandBus>();
            // Register CommandHandlers
            container.RegisterAssemblyTypes(typeof(Startup).GetTypeInfo().Assembly)
              .AsClosedTypesOf(typeof(IHandleCommand<>))
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();
              })
              .UseConsoleLifetime()
              .Build()
              .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration((hostingContext, config) => {
                  config.SetBasePath(Directory.GetCurrentDirectory());
                  config.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false);
                  config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                  config.AddJsonFile("/run/secrets/connectioninfo.json", optional: true, reloadOnChange: true);
                  config.AddKeyPerFile(directoryPath: "/kvmnt", optional: true);
                  config.AddUserSecrets<Startup>();
              })
              .ConfigureLogging((hostingContext, logging) => {
                  logging.AddConsole();
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
              })
              .ConfigureWebHostDefaults(webBuilder => {
                  webBuilder.ConfigureKestrel(options => {
                      options.AddServerHeader = true;
                  // Setup a HTTP/2 endpoint without TLS.
                  options.ListenLocalhost(5000, o => {
                          o.Protocols = HttpProtocols.Http2;
                      });
                  });
                  webBuilder.UseStartup<Startup>();
              });
    }
}