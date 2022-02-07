using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.App.ViewModel
{
    public class MainViewModel
    {
        public VideoGameConsolesViewModel VideoGameConsoles { get; set; }
        public VideoGamesViewModel VideoGames { get; set; }


        public MainViewModel()
        {
            this.VideoGameConsoles = new VideoGameConsolesViewModel();
            this.VideoGames = new VideoGamesViewModel();
        }
    }
}
