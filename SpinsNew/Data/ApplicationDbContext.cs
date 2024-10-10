using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpinsNew.Libraries;
using SpinsNew.Models;
using System;
//using MySQL.EntityFrameworkCore.Extensions;

namespace SpinsNew.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PayrollModel> tbl_payroll_socpen { get; set; }
        public DbSet<MasterListModel> tbl_masterlist { get; set; }
        public DbSet<SpbufModel> tbl_spbuf { get; set; }
        public DbSet<LogModel> log_masterlist { get; set; }
        public DbSet<GisModel> tbl_gis { get; set; } //Database first so reference the name of database into our actual database.
        public DbSet<RegisterModel> tbl_registered_users { get; set; }
        public DbSet<AttachmentModel> tbl_attachments { get; set; }
        public DbSet<TableAuthRepresentative> tbl_auth_representative { get; set; }

        //Libraries classes below
        public DbSet<LibraryRegion> lib_region_fortesting { get; set; }
        public DbSet<LibraryValidator> lib_validator { get; set; }
        public DbSet<LibraryAssessment> lib_assessment { get; set; }
        public DbSet<LibraryRegistrationType> lib_registration_type { get; set; }
        public DbSet<LibraryDataSource> lib_datasource { get; set; }
        public DbSet<LibraryMaritalStatus> lib_marital_status { get; set; }
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
        public DbSet<LibraryRelationship> lib_relationship { get; set; }
        public DbSet<LibraryRole> LibraryRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerfactory = LoggerFactory.Create(builder => builder.AddConsole());
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseLoggerFactory(loggerfactory)
            .UseMySQL(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*This mapping below is for Indexing.
             Instead of searching the whole table from tbl_payroll_socpen
            search the data that is given instead, it will cut the time searching
            instead of scanning all the row into database.*/
            modelBuilder.Entity<RegisterModel>()
                .HasIndex(e => new
                {
                    e.Username
                });

            modelBuilder.Entity<PayrollModel>()
                .HasIndex(e => new 
                { 
                    e.Year, 
                    e.ClaimTypeID,
                    e.PeriodID,
                    e.PayrollStatusID,
                    e.MasterListID
                    
                });

            modelBuilder.Entity<MasterListModel>()
                .HasIndex(m => new 
                {
                    m.Id,
                    m.LastName,
                    m.FirstName,
                    m.MiddleName,
                    m.ExtName,
                    m.PSGCCityMun, 
                    m.StatusID, 
                    m.DateTimeDeleted,
                    m.RegTypeId
                });

            //Indexing for Register Model.
            modelBuilder.Entity<RegisterModel>()
                .HasIndex(u => new
                {
                    u.Username,
                    u.Password
                });

            modelBuilder.Entity<LogModel>()
                .HasIndex(id => id.MasterListId);

            /*Fluent API mapping below*/
            modelBuilder.Entity<RegisterModel>()
                .HasOne(l => l.LibraryRole)
                .WithMany(r => r.RegisterModels)
                .HasForeignKey(r => r.UserRole)
                .HasPrincipalKey(l => l.UserRoleId);

            modelBuilder.Entity<TableAuthRepresentative>()
                .HasOne(m => m.GisModel)
                .WithMany(t => t.TableAuthRepresentatives)
                .HasForeignKey(t => t.ReferenceCode)
                .HasPrincipalKey(m => m.ReferenceCode);

            modelBuilder.Entity<TableAuthRepresentative>()
                .HasMany(r => r.LibraryRelationships)
                .WithOne(a => a.TableAuthRepresentative)
                .HasForeignKey(r => r.Id)
                .HasPrincipalKey(a => a.RelationshipId);

            modelBuilder.Entity<GisModel>()
                .HasOne(m => m.MasterListModel)
                .WithMany(g => g.GisModels)
                .HasForeignKey(g => g.MasterListID)
                .HasPrincipalKey(m => m.Id);

            modelBuilder.Entity<GisModel>()
                .HasOne(la => la.LibraryAssessment)
                .WithMany(g => g.GisModels)
                .HasForeignKey(g => g.AssessmentID)
                .HasPrincipalKey(la => la.Id);

            modelBuilder.Entity<GisModel>()
                .HasOne(lv => lv.LibraryValidator)
                .WithMany(g => g.gisModels)
                .HasForeignKey(g => g.ValidatedByID)
                .HasPrincipalKey(lv => lv.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasMany(a => a.AttachmentModels)
                .WithOne(m => m.MasterListModel)
                .HasForeignKey(a => a.MasterListId)
                .HasPrincipalKey(m => m.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasMany(log => log.LogModels)//Logs
                .WithOne(m => m.masterListModel)//Masterlist
                .HasForeignKey(log => log.MasterListId)//Logs
                .HasPrincipalKey(m => m.Id);//Masterlist

            modelBuilder.Entity<MasterListModel>()
                .HasMany(sp => sp.SpbufModels)
                .WithOne(m => m.MasterListModel)
                .HasForeignKey(sp => sp.MasterListId)
                .HasPrincipalKey(m => m.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(rt => rt.LibraryRegistrationType)
                .WithMany(m => m.MasterListModels)
                .HasForeignKey(m => m.RegTypeId)
                .HasPrincipalKey(rt => rt.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(d => d.LibraryDataSource)
                .WithMany(m => m.MasterListModels)
                .HasForeignKey(m => m.DataSourceId)
                .HasPrincipalKey(d => d.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(lr => lr.LibraryRegion)
                .WithMany(m => m.masterListModels)
                .HasForeignKey(m => m.PSGCRegion)
                .HasPrincipalKey(lr => lr.PSGCRegion);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(lp => lp.LibraryProvince)
                .WithMany(m => m.MasterListModels)
                .HasForeignKey(m => m.PSGCProvince)
                .HasPrincipalKey(lp => lp.PSGCProvince);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(m => m.LibraryMunicipality)
                .WithMany(ma => ma.MasterListModels)
                .HasForeignKey(ma => ma.PSGCCityMun)
                .HasPrincipalKey(m => m.PSGCCityMun);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(b => b.LibraryBarangay)
                .WithMany(m => m.masterListModels)
                .HasForeignKey(m => m.PSGCBrgy)
                .HasPrincipalKey(b => b.PSGCBrgy);

            modelBuilder.Entity<MasterListModel>()
                .HasOne(m => m.LibraryMaritalStatus)
                .WithMany(ml => ml.masterListModels)
                .HasForeignKey(ml => ml.MaritalStatusID)
                .HasPrincipalKey(m => m.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.MasterListModel)
                .HasForeignKey(e => e.MasterListID)
                .HasPrincipalKey(e => e.Id);


            modelBuilder.Entity<MasterListModel>()
                .HasMany(g => g.GisModels)
                .WithOne(m => m.MasterListModel)
                .HasForeignKey(g => g.MasterListID)
                .HasPrincipalKey(m => m.Id);

            modelBuilder.Entity<MasterListModel>()
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.MasterListModel)
                .HasForeignKey(e => e.MasterListID)
                .HasPrincipalKey(e => e.Id);



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


            //modelBuilder.Entity<LibraryMunicipality>()
            //    .HasOne(p => p.LibraryProvince)
            //    .WithMany(c => c.LibraryMunicipalities)
            //    .HasForeignKey(c => c.PSGCProvince)
            //    .HasPrincipalKey(p => p.PSGCProvince);

            //Cascading the Region, Province, Municipality, Barangay below.
            modelBuilder.Entity<LibraryRegion>()
                .HasMany(lp => lp.LibraryProvinces)
                .WithOne(lr => lr.LibraryRegion)
                .HasForeignKey(lp => lp.PSGCRegion)
                .HasPrincipalKey(lr => lr.PSGCRegion);

            modelBuilder.Entity<LibraryProvince>()
                .HasMany(lm => lm.LibraryMunicipalities)
                .WithOne(lp => lp.LibraryProvince)
                .HasForeignKey(lm => lm.PSGCProvince)
                .HasPrincipalKey(lp => lp.PSGCProvince);

            modelBuilder.Entity<LibraryMunicipality>()
                .HasMany(lb => lb.LibraryBarangays)
                .WithOne(lm => lm.LibraryMunicipality)
                .HasForeignKey(lb => lb.PSGCCityMun)
                .HasPrincipalKey(lm => lm.PSGCCityMun);

            modelBuilder.Entity<LibraryBarangay>()
                 .HasMany(e => e.PayrollModels)
                 .WithOne(e => e.LibraryBarangay)
                 .HasForeignKey(e => e.PSGCBrgy)
                 .HasPrincipalKey(e => e.PSGCBrgy);

            base.OnModelCreating(modelBuilder);
        }


    }
}
