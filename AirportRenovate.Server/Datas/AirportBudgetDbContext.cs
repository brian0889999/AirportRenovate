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
        //public DbSet<LoginViewModelDb> Money { get; set; }
        //public DbSet<LoginViewModelDb> Money2 { get; set; }
        //public DbSet<LoginViewModelDb> Money3 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
