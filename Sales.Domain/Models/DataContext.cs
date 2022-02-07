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

        public System.Data.Entity.DbSet<VideoGameConsole> VideoGameConsoles { get; set; }

        public System.Data.Entity.DbSet<VideoGames> VideoGames { get; set; }
    }
}
