using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();

#if DEBUG
    Console.WriteLine("Mode=Debug"); 
    DotNetEnv.Env.Load();
#else
    Console.WriteLine("Mode=Release"); 
#endif

builder.ConfigureServices(services =>
{
    services.AddHttpClient<MovieDbService>();
    services.AddDbContextPool<AppDbContext>(options =>
    {
        options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECT_STRING"));
    });
}).ConfigureLogging(logging =>
{
    logging.AddFilter("Azure.Core", LogLevel.Warning);
    logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
    logging.AddFilter("System.Net.Http.HttpClient", LogLevel.Warning);
});

var host = builder.Build();

host.Run();
