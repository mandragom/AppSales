using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<VideoGameConsole> VideoGameConsoles { get; set; }

        public DbSet<VideoGames> VideoGames { get; set; }
    }
}
