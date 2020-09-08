using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaidEasy.Models
{
    public class CustomDbContext : DbContext
    {
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<contactu> contactus { get; set; }
        public virtual DbSet<contract> contracts { get; set; }
        public virtual DbSet<thana> thana { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<work> work { get; set; }
        public virtual DbSet<worker> worker { get; set; }
        public virtual DbSet<workerreview> workerreview { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=maideasy;user=root;password=");

        }
    }
}