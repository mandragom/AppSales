using DomainSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendSales.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<CommonSales.Models.Product> Products { get; set; }
    }
}