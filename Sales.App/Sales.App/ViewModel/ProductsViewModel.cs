using CommonSales.Models;
using GalaSoft.MvvmLight.Command;
using Sales.App.Helpers;
using Sales.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.App.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService _apiservice;
        private bool _isrefreshing;
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products {
            get { return this._products; }
            set { this.SetValue(ref this._products, value);  }
        }

        public bool IsRefreshing
        {
            get { return this._isrefreshing; }
            set { this.SetValue(ref this._isrefreshing, value); }
        }

        public ProductsViewModel()
        {
            this._apiservice = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            //Validate internet connection
            var connection = await this._apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            //Get the product list
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this._apiservice.GetList<Product>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<Product> list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get 
            {
                return new RelayCommand(LoadProducts);
            }
       }
    }
}
