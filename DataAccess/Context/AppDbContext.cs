using Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        // 1. Veritabanı Bağlantı Ayarı
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Buradaki "Server=..." kısmına kendi SQL Server adını yazmalısın.
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ITHelpDeskDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // 2. Tabloların Tanımlanması
        // "Benim Category sınıfımı git veritabanında Categories tablosuna dönüştür" diyoruz.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
