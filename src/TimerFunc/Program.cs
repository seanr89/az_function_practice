using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();


builder.ConfigureServices(services =>
{
    services.AddTransient<DataService>();
    services.AddHttpClient<MovieDbService>();
});

var host = builder.Build();

host.Run();
