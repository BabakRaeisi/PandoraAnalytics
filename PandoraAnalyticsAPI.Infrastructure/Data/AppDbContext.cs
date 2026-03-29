using Microsoft.EntityFrameworkCore;
using PandoraAnalyticsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Trial> Trials => Set<Trial>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasKey(p => p.PlayerId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Player)
                .WithMany(p => p.Sessions)
                .HasForeignKey(s => s.PlayerId)
                .HasPrincipalKey(p => p.PlayerId);

            modelBuilder.Entity<Trial>()
                .HasOne(t => t.Session)
                .WithMany(s => s.Trials)
                .HasForeignKey(t => t.SessionId);
        }
    }
}
