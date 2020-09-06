using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MaidEasy.Models
{
    public class ThanaContext : DbContext
    {
        public DbSet<thana> thana { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=maideasy;user=root;password=");
            
        }
    }
}