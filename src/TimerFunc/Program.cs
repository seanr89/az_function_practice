using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
    services.AddSingleton<DataService>();
    services.AddHttpClient<MovieDbService>();
    services.AddSingleton<AppRunner>();
    services.AddSingleton<PersonUpdater>();
    services.AddDbContextPool<AppDbContext>(options =>
    {
        options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECT_STRING"));
    });
});

var host = builder.Build();

host.Run();
