using Sales.App.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.App.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
