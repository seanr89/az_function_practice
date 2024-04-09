using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();

#if DEBUG
    Console.WriteLine("Mode=Debug"); 
#else
    Console.WriteLine("Mode=Release"); 
#endif

builder.ConfigureServices(services =>
{
    services.AddTransient<DataService>();
    services.AddHttpClient<MovieDbService>();
});

var host = builder.Build();

host.Run();
