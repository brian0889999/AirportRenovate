using AirportRenovate.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace AirportRenovate.Server.Datas
{
    public class AirportBudgetDbContext : DbContext
    {
        public AirportBudgetDbContext(DbContextOptions<AirportBudgetDbContext> options) : base(options)
        {
        }
        public DbSet<LoginViewModelDb> user_data1 {  get; set; }
        public DbSet<MoneyDbModel> Money { get; set; }
        public DbSet<Money2DbModel> Money2 { get; set; }
        public DbSet<Money3DbModel> Money3 { get; set; }
        public DbSet<Type1DbModel> Type1 { get; set; }
        public DbSet<Type2DbModel> Type2 { get; set; }
        public DbSet<Type3DbModel> Type3 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
