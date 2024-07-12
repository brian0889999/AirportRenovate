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
        public DbSet<User> User_data1 {  get; set; }
        public DbSet<Money> Money { get; set; }
        public DbSet<Money2> Money2 { get; set; }
        public DbSet<Money3> Money3 { get; set; }
        public DbSet<Type1> Type1 { get; set; }
        public DbSet<Type2> Type2 { get; set; }
        public DbSet<Type3> Type3 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 設定主鍵
            modelBuilder.Entity<Money>().HasKey(m => m.ID);
            modelBuilder.Entity<Money3>().HasKey(m3 => m3.ID);
            // 設定關聯
            //modelBuilder.Entity<MoneyDbModel>()
            //    .HasMany(m => m.Money3s)
            //    .WithOne(m3 => m3.Money)
            //    .HasForeignKey(m3 => m3.Name) // 注意這裡的外鍵應該對應到正確的屬性
            //    .HasPrincipalKey(m => m.Budget);

            modelBuilder.Entity<Money3>()
                .HasOne(m3 => m3.Money)
                .WithMany(m => m.Money3s)
                .HasForeignKey(m3 => m3.Name) 
                .HasPrincipalKey(m => m.Budget);


            //modelBuilder.Entity<Money3DbModel>()
            //    .HasOne(m3 => m3.MoneyDbModel)
            //    .WithMany()
            //    .HasForeignKey(m3 => m3.Name)
            //    .HasPrincipalKey(m => m.Budget);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Money;MultipleActiveResultSets=True;");
        //    }
        //}
    }
}