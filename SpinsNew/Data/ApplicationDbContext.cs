using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpinsNew.Libraries;
using SpinsNew.Models;
using SpinsNew.ViewModel;
using System;
//using MySQL.EntityFrameworkCore.Extensions;

namespace SpinsNew.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Public constructor for creating instances directly
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}

        //public ApplicationDbContext()
        //{
        //}
        

        // Override OnConfiguring to set up the connection string (if not using dependency injection)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          

            if (!optionsBuilder.IsConfigured)
            {

                var connectionString = "Server=172.26.153.181;uid=spinsv3;Password=Pn#z800^*OsR6B0;Database=caraga-spins2;default command timeout=3600;Allow User Variables=True";
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                optionsBuilder.UseLoggerFactory(loggerFactory)
                    //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    //.UseLoggerFactory(loggerFactory)
                    .UseMySQL(connectionString);
                  //  .Options;
                /*
                optionsBuilder
                .UseMySQL(connectionString);
                //.EnableSensitiveDataLogging(Console.WriteLine, LogLevel.Information);
                */
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*This mapping below is for Indexing.
             Instead of searching the whole table from tbl_payroll_socpen
            search the data that is given instead, it will cut the time searching
            instead of scanning all the row into database.*/
            modelBuilder.Entity<PayrollModel>()
                .HasIndex(e => new { e.Year, e.ClaimTypeID, e.PeriodID });


            modelBuilder.Entity<PayrollModel>()
                .HasIndex(e => e.Year);
            modelBuilder.Entity<PayrollModel>()
                .HasIndex(e => e.ClaimTypeID);
            modelBuilder.Entity<PayrollModel>()
                .HasIndex(e => e.PeriodID);

            /*Fluent API mapping below*/

            modelBuilder.Entity<MasterListModel>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.MasterListModel)
                .HasForeignKey(e => e.MasterListID)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<LibraryBarangay>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryBarangay)
                .HasForeignKey(e => e.PSGCBrgy)
                .HasPrincipalKey(e => e.PSGCBrgy);

            modelBuilder.Entity<MasterListModel>()
                 .HasOne(e => e.LibrarySex)
                 .WithMany(e => e.MasterListModels)
                 .HasForeignKey(e => e.SexID)
                 .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<MasterListModel>()
               .HasOne(e => e.LibraryHealthStatus)
               .WithMany(e => e.MasterListModels)
               .HasForeignKey(e => e.HealthStatusID)
               .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<MasterListModel>()
                 .HasOne(e => e.LibraryIDType)
                 .WithMany(e => e.MasterListModels)
                 .HasForeignKey(e => e.IDtypeID)
                 .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<LibraryPeriod>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryPeriod)
                .HasForeignKey(e => e.PeriodID)
                .HasPrincipalKey(e => e.PeriodID);

            modelBuilder.Entity<LibraryPayrollStatus>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryPayrollStatus)
                .HasForeignKey(e => e.PayrollStatusID)
                .HasPrincipalKey(e => e.PayrollStatusID);
            modelBuilder.Entity<LibraryClaimType>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryClaimType)
                .HasForeignKey(e => e.ClaimTypeID)
                .HasPrincipalKey(e => e.ClaimTypeID);
            modelBuilder.Entity<LibraryPayrollType>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryPayrollType)
                .HasForeignKey(e => e.PayrollTypeID)
                .HasForeignKey(e => e.PayrollTypeID);
            modelBuilder.Entity<LibraryPayrollTag>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryPayrollTag)
                .HasForeignKey(e => e.PayrollTagID)
                .HasPrincipalKey(e => e.PayrollTagID);
            modelBuilder.Entity<MasterListModel>()
                .HasOne(e => e.LibraryStatus)
                .WithMany(e => e.MasterListModels)
                .HasForeignKey(e => e.StatusID)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<LibraryPaymentMode>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryPaymentMode)
                .HasForeignKey(e => e.PaymentModeID)
                .HasForeignKey(e => e.PaymentModeID);
            modelBuilder.Entity<LibraryProvince>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryProvince)
                .HasForeignKey(e => e.PSGCProvince)
                .HasPrincipalKey(e => e.PSGCProvince);

            modelBuilder.Entity<LibraryMunicipality>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.LibraryMunicipality)
                .HasForeignKey(e => e.PSGCCityMun)
                .HasPrincipalKey(e => e.PSGCCityMun);

            modelBuilder.Entity<LibraryYear>()
                .HasMany(x => x.PayrollModels)
                .WithOne(x => x.LibraryYear)
                .HasForeignKey(x => x.Year)//Referenced from our payrollmodels.
                .HasPrincipalKey(x => x.Id);//referenced from our lib_year

            //modelBuilder.Entity<GisModel>()
            //    .HasMany(x => x.PayrollModels)
            //    .WithOne(x => x.GisModel)
            //    .HasForeignKey(x => x.MasterListID)
            //    .HasPrincipalKey(x => x.MasterListID);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(g => g.GisModel)
                .WithMany(m => m.MasterListModels)
                .HasForeignKey(m => m.Id)
                .HasPrincipalKey(g => g.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.MasterListModel)
                .HasForeignKey(e => e.MasterListID)
                .HasPrincipalKey(e => e.Id);
        
            //Many to many
            //modelBuilder.Entity<PayrollandGisManyToMany>()
            //    .HasKey(pg => new { pg.PayrollModeID, pg.GisModeID });

            //modelBuilder.Entity<PayrollandGisManyToMany>()
            //    .HasOne(pg => pg.PayrollModel)
            //    .WithMany(p => p.PayrollandGisManyToManys)
            //    .HasForeignKey(pg => pg.PayrollModeID);

            //modelBuilder.Entity<PayrollandGisManyToMany>()
            //    .HasOne(pg => pg.GisModel)
            //    .WithMany(p => p.PayrollandGisManyToManys)
            //    .HasForeignKey(pg => pg.GisModeID);
            //modelBuilder.Entity<PayrollModel>()
            //    .HasMany(e => e.GisModels)
            //    .WithMany(e => e.)
                

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PayrollModel> tbl_payroll_socpen { get; set; }
        public DbSet<MasterListModel> tbl_masterlist { get; set; }

        //Libraries classes below
        public DbSet<LibraryBarangay> lib_barangay { get; set; }
        public DbSet<LibrarySex> lib_sex { get; set; }
        public DbSet<LibraryHealthStatus> lib_health_status { get; set; }
        public DbSet<LibraryIDType> lib_id_type { get; set; }
        public DbSet<LibraryPeriod> lib_period { get; set; }
        public DbSet<LibraryPayrollStatus> lib_payroll_status { get; set; }
        public DbSet<LibraryClaimType> lib_claim_type { get; set; }
        public DbSet<LibraryPayrollType> lib_payroll_type { get; set; }
        public DbSet<LibraryPayrollTag> lib_payroll_tag { get; set; }
        public DbSet<LibraryStatus> lib_status { get; set; }
        public DbSet<LibraryPaymentMode> lib_payment_mode { get; set; }
        public DbSet<LibraryProvince> lib_province { get; set; }
        public DbSet<LibraryMunicipality> lib_city_municipality { get; set; }
        public DbSet<LibraryYear> lib_year { get; set; }
        public DbSet<GisModel> tbl_gis { get; set; } //Database first so reference the name of database into our actual database.

    }
}
