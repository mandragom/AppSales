using CommonSales.Models;
using GalaSoft.MvvmLight.Command;
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
            //var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this._apiservice.GetList<Product>("https://02e8-2806-2f0-9020-a470-6147-ec22-e57b-7e00.ngrok.io", "/api", "/Products");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
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
