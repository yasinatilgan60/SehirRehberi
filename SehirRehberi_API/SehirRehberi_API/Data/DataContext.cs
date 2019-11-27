using Microsoft.EntityFrameworkCore;
using SehirRehberi_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi_API.Data
{
    public class DataContext:DbContext 
    {
        // base - dbcontext
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {

        }
        public DbSet<Value> Values { get; set; } // Mapping yapılmaz ise isimlerin aynı olması gerekir. 
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
