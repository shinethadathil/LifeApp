using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeApp.API.Models;
using Microsoft.EntityFrameworkCore;


namespace LifeApp.API.Data
{
    public class DataContext : DbContext
    {
        // initialize DbContextOptions in the constructor and make a base constructor call
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        //initialize DbSets
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<User> Users { get; set; }
        
        
    }
}
