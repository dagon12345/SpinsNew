using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpinsNew.DataAccess.Models;

namespace SpinsNew.DataAccess.FluentConfiguration
{
    internal class ApplicationFluentConfiguration : IEntityTypeConfiguration<MasterListModel>
    {

        public void Configure(EntityTypeBuilder<MasterListModel> modelBuilder)
        {
            modelBuilder
                .HasIndex(m => new { m.PSGCCityMun, m.StatusID });


            modelBuilder
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.MasterListModel)
                .HasForeignKey(e => e.MasterListID)
                .HasPrincipalKey(e => e.Id);

            modelBuilder
                 .HasOne(e => e.LibrarySex)
                 .WithMany(e => e.MasterListModels)
                 .HasForeignKey(e => e.SexID)
                 .HasPrincipalKey(e => e.Id);

            modelBuilder
                 .HasOne(e => e.LibraryHealthStatus)
                 .WithMany(e => e.MasterListModels)
                 .HasForeignKey(e => e.HealthStatusID)
                 .HasPrincipalKey(e => e.Id);

            modelBuilder
                 .HasOne(e => e.LibraryIDType)
                 .WithMany(e => e.MasterListModels)
                 .HasForeignKey(e => e.IDtypeID)
                 .HasPrincipalKey(e => e.Id);

            modelBuilder
                .HasOne(m => m.LibraryMunicipality)
                .WithMany(ma => ma.MasterListModels)
                .HasForeignKey(ma => ma.PSGCCityMun)
                .HasPrincipalKey(m => m.PSGCCityMun);

            modelBuilder
                .HasOne(e => e.LibraryStatus)
                .WithMany(e => e.MasterListModels)
                .HasForeignKey(e => e.StatusID)
                .HasPrincipalKey(e => e.Id);

            modelBuilder
                .HasOne(g => g.GisModel)
                .WithMany(m => m.MasterListModels)
                .HasForeignKey(m => m.Id)
                .HasPrincipalKey(g => g.Id);

            modelBuilder
                .HasMany(e => e.PayrollModels)
                .WithOne(e => e.MasterListModel)
                .HasForeignKey(e => e.MasterListID)
                .HasPrincipalKey(e => e.Id);


        }

    }
}
