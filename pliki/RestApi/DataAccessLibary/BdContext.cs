using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibary
{
    public class BdContext : DbContext
    {
        public BdContext(DbContextOptions options) : base(options) { }
        public DbSet<Order> Order { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().ToTable("Zamówienia");

            modelBuilder.Entity<Order>().Property(x => x.IDzamówienia).HasColumnName("IDzamówienia");
            modelBuilder.Entity<Order>().Property(x => x.IDklienta).HasColumnName("NazwaKolumnyWTwojejBazieDanych2");
            modelBuilder.Entity<Order>().Property(x => x.IDpracownika).HasColumnName("IDpracownika");
            modelBuilder.Entity<Order>().Property(x => x.DataZamówienia).HasColumnName("DataZamówienia");
            modelBuilder.Entity<Order>().Property(x => x.DataWymagania).HasColumnName("DataWymagana");
            modelBuilder.Entity<Order>().Property(x => x.DataWysylki).HasColumnName("DataWysyłki");
            modelBuilder.Entity<Order>().Property(x => x.IDspedytora).HasColumnName("IDspedytora");
            modelBuilder.Entity<Order>().Property(x => x.Fracht).HasColumnName("Fracht");
            modelBuilder.Entity<Order>().Property(x => x.NazwaOdbiorcy).HasColumnName("NazwaOdbiorcy");
            modelBuilder.Entity<Order>().Property(x => x.AdresOdbiorcy).HasColumnName("AdresOdbiorcy");
            modelBuilder.Entity<Order>().Property(x => x.MiastoOdbiorcy).HasColumnName("MiastoOdbiorcy");
            modelBuilder.Entity<Order>().Property(x => x.RegionOdbiorcy).HasColumnName("RegionOdbiorcy");
            modelBuilder.Entity<Order>().Property(x => x.KodPocztowy).HasColumnName("KodPocztowyOdbiorcy");
            modelBuilder.Entity<Order>().Property(x => x.KrajOdbiorcy).HasColumnName("KrajOdbiorcy");
            //itd. dla każdej kolumny w tabeli
        }

    }
}
