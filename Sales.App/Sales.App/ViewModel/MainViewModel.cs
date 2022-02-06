using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.App.ViewModel
{
    public class MainViewModel
    {
        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
        }
    }
}
