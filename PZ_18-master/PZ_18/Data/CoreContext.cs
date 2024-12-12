using Microsoft.EntityFrameworkCore;
using PZ_18.Models;

namespace PZ_18.Data
{
    /// <summary>
    /// EF Core контекст базы данных.
    /// Настройка подключения к БД через UseSqlServer.
    /// </summary>
    public class CoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<HomeTechType> HomeTechTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RepairPart> RepairParts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public CoreContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Строка подключения к локальному экземпляру SQL Server.
                optionsBuilder.UseSqlServer("Server=DESKTOP-Q11QP9V\\SQLEXPRESS;Database=ServiceCenterDB;Trusted_Connection=True;TrustServerCertificate=True;");

				// Строка подключения к локальному экземпляру SQL Server, через колледж
				//optionsBuilder.UseSqlServer("Server=192.168.147.54;Database=ServiceCenterDB1;User Id=is;Password=1;TrustServerCertificate=True;");
			}
		}
    }
}