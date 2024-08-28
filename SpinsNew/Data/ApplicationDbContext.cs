using Microsoft.EntityFrameworkCore;
using SpinsNew.Libraries;
using SpinsNew.Models;
//using MySQL.EntityFrameworkCore.Extensions;

namespace SpinsNew.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Public constructor for creating instances directly
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        // Override OnConfiguring to set up the connection string (if not using dependency injection)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=172.26.153.181;uid=spinsv3;Password=Pn#z800^*OsR6B0;Database=caraga-spins2;default command timeout=3600;Allow User Variables=True");
            }
        }

        public DbSet<PayrollModel> tbl_payroll_socpen { get; set; }
        public DbSet<LibraryClaimType> lib_claim_type { get; set; }

    }
}
