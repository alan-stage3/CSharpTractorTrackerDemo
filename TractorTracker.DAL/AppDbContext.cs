using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TractorTracker.Core.Entities;

namespace TractorTracker.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pull> Pull { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Tractor> Tractor { get; set; }
        public DbSet<PullTractorDriver> PullTractorDriver { get; set; }


        public AppDbContext() : base()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PullTractorDriver>()
                .HasKey(ptd => new { ptd.pullId, ptd.driverId, ptd.tractorId });

            builder.Entity<Tractor>()
                 .HasMany(d => d.drivers)
                 .WithMany(t => t.tractors)
                 .UsingEntity<Dictionary<string, object>>(
                "TractorDriver",
                j => j
                    .HasOne<Driver>()
                    .WithMany()
                    .HasForeignKey("driverID")
                    .HasConstraintName(" fk_TractorDriver_driverId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Tractor>()
                    .WithMany()
                    .HasForeignKey("tractorId")
                    .HasConstraintName(" fk_TractorDriver_tractorId")
                    .OnDelete(DeleteBehavior.Cascade));

            builder.Entity<Driver>()
                 .HasMany(d => d.tractors)
                 .WithMany(t => t.drivers)
                 .UsingEntity<Dictionary<string, object>>(
                "TractorDriver",
                j => j
                    .HasOne<Tractor>()
                    .WithMany()
                    .HasForeignKey("tractorId")
                    .HasConstraintName(" fk_TractorDriver_tractorId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Driver>()
                    .WithMany()
                    .HasForeignKey("driverID")
                    .HasConstraintName(" fk_TractorDriver_driverId")
                    .OnDelete(DeleteBehavior.Cascade));



        }
    }
}
