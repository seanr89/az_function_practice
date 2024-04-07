using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();


builder.ConfigureServices(services =>
{
    services.AddTransient<DataService>();
});

var host = builder.Build();

host.Run();
