using Sales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.Backend.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Sales.Common.Models.VideoGameConsole> VideoGameConsoles { get; set; }

        public System.Data.Entity.DbSet<CommonSales.Models.VideoGames> VideoGames { get; set; }
    }
}