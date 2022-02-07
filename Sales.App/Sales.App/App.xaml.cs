using Sales.App.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sales.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new VideoGameConsolesPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
