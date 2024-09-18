using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNewRepository
{
    static class Program
    {

        // Properly typed ServiceProvider as IServiceProvider
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            static IHostBuilder CreateHostBuilder()
            {
                return Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                    //Database configuration
                    services.AddScoped<ApplicationDbContext>();
                });
            }
        }
    }
}
