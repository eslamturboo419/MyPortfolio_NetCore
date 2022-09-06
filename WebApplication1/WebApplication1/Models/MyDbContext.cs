using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication1.Models
{
    public class MyDbContext:DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// when he create table 'owner' it add a NEWID() when he add a new item
            modelBuilder.Entity<Owner>().Property(x=>x.Id).HasDefaultValueSql("NEWID()");

            /// when he create table 'PortfolioItem' it add a NEWID() when he add a new item
            modelBuilder.Entity<PortfolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");


            //// add new item when he create table
            modelBuilder.Entity<Owner>().HasData(new Owner()
            {
                Id =  Guid.NewGuid() , FullName="Eslam Akrm",  Avatar="Pic.jpg" , Profilo=".NET Developer"  
            });

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Owner>  Owners  { get; set; }
        public DbSet<PortfolioItem>    portfolioItems  { get; set; }
    


    }
}
