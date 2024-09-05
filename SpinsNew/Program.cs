using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpinsNew.Forms;
using SpinsNew.Interfaces;
using SpinsNew.Services;
using SpinsNew.StatisticsForm;
using System;
using System.Windows.Forms;



namespace SpinsNew
{
    static class Program
    {
        // Properly typed ServiceProvider as IServiceProvider
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
        
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    /*AddTransient
                        Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
 
                        AddScoped
                        Scoped lifetime services are created once per request.
                         
                        AddSingleton
                        Singleton lifetime services are created the first time they are requested (or when ConfigureServices is run if you specify an instance there) and then every subsequent request will use the same instance.*/
                    services.AddSingleton<ITablePayroll, TablePayroll>();
                    services.AddSingleton<PaidStatisticsForm>();
                });
        }
    }
}
