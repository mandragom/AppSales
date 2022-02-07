using Sales.Common.Models;
using Sales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.Backend.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<VideoGameConsole> VideoGameConsoles { get; set; }

        public System.Data.Entity.DbSet<VideoGames> VideoGames { get; set; }
    }
}