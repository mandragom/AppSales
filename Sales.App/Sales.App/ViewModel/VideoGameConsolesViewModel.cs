using Sales.Common.Models;
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
    public class VideoGameConsolesViewModel : BaseViewModel
    {
        private ApiService _apiservice;
        private bool _isrefreshing;
        private ObservableCollection<VideoGameConsole> _videogameconsole;

        public ObservableCollection<VideoGameConsole> VideoGameConsole {
            get { return this._videogameconsole; }
            set { this.SetValue(ref this._videogameconsole, value);  }
        }

        public bool IsRefreshing
        {
            get { return this._isrefreshing; }
            set { this.SetValue(ref this._isrefreshing, value); }
        }

        public VideoGameConsolesViewModel()
        {
            this._apiservice = new ApiService();
            this.LoadVideoGameConsoles();
        }

        private async void LoadVideoGameConsoles()
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
            var controller = Application.Current.Resources["UrlVideoGameConsolesController"].ToString();

            var response = await this._apiservice.GetList<VideoGameConsole>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<VideoGameConsole> list = (List<VideoGameConsole>)response.Result;
            this.VideoGameConsole = new ObservableCollection<VideoGameConsole>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get 
            {
                return new RelayCommand(LoadVideoGameConsoles);
            }
       }
    }
}
