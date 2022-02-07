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
    public class VideoGamesViewModel : BaseViewModel
    {
        private ApiService _apiservice;
        private bool _isrefreshing;
        private ObservableCollection<VideoGames> _videogames;

        public ObservableCollection<VideoGames> VideoGame
        {
            get { return this._videogames; }
            set { this.SetValue(ref this._videogames, value); }
        }

        public bool IsRefreshing
        {
            get { return this._isrefreshing; }
            set { this.SetValue(ref this._isrefreshing, value); }
        }

        public VideoGamesViewModel()
        {
            this._apiservice = new ApiService();
            this.LoadVideoGames();
        }

        private async void LoadVideoGames()
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
            var controller = Application.Current.Resources["UrlVideoGames"].ToString();

            var response = await this._apiservice.GetList<VideoGames>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<VideoGames> list = (List<VideoGames>)response.Result;
            this.VideoGame = new ObservableCollection<VideoGames>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadVideoGames);
            }
        }
    }

}
