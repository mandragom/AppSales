using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.App.ViewModel
{
    public class MainViewModel
    {
        public VideoGameConsolesViewModel VideoGameConsoles { get; set; }






        #region Videogame

        public VideoGamesViewModel VideoGames { get; set; }
        public AddVideoGameViewModel AddVideogame { get; set; }
        
        
        public ICommand AddVideoGame
        {
            get
            {
                return new RelayCommand(GoToAddVideoGame);
            }
        }

        private async void GoToAddVideoGame()
        {
            this.AddVideogame = new AddVideoGameViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddVideoGamePage());
        }

        #endregion

        public MainViewModel()
        {
            this.VideoGameConsoles = new VideoGameConsolesViewModel();
            this.VideoGames = new VideoGamesViewModel();
        }

    }
}
