using AirportRenovate.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;


namespace AirportRenovate.Server.Datas
{
    public class AirportBudgetDbContext : DbContext
    {
        public AirportBudgetDbContext(DbContextOptions<AirportBudgetDbContext> options) : base(options)
        {
        }
        public DbSet<Users> User_data1 {  get; set; }
        public DbSet<MoneyDbModel> Money { get; set; }
        public DbSet<Money2DbModel> Money2 { get; set; }
        public DbSet<Money3DbModel> Money3 { get; set; }
        public DbSet<Type1DbModel> Type1 { get; set; }
        public DbSet<Type2DbModel> Type2 { get; set; }
        public DbSet<Type3DbModel> Type3 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 設定主鍵
            modelBuilder.Entity<MoneyDbModel>().HasKey(m => m.ID);
            modelBuilder.Entity<Money3DbModel>().HasKey(m3 => m3.ID);
            // 設定關聯
            //modelBuilder.Entity<MoneyDbModel>()
            //    .HasMany(m => m.Money3DbModels)
            //    .WithOne(m3 => m3.MoneyDbModel)
            //    .HasForeignKey(m3 => m3.Name) // 注意這裡的外鍵應該對應到正確的屬性
            //    .HasPrincipalKey(m => m.Budget);

            modelBuilder.Entity<Money3DbModel>()
                .HasOne(m3 => m3.MoneyDbModel)
                .WithMany(m => m.Money3DbModels)
                .HasForeignKey(m3 => m3.Name) 
                .HasPrincipalKey(m => m.Budget);


            //modelBuilder.Entity<Money3DbModel>()
            //    .HasOne(m3 => m3.MoneyDbModel)
            //    .WithMany()
            //    .HasForeignKey(m3 => m3.Name)
            //    .HasPrincipalKey(m => m.Budget);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Money;MultipleActiveResultSets=True;");
            }
        }
    }
}