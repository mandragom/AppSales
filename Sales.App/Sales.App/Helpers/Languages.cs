using Sales.App.Interfaces;
using Sales.App.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sales.App.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string VideoGameConsoles
        {
            get { return Resource.VideoGameConsoles; }
        }

        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }
        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }

        #region Videogames

        public static string VideoGames
        {
            get { return Resource.VideoGames; }
        }

        public static string AddVideogame
        {
            get { return Resource.AddVideogame; }
        }

        public static string Console
        {
            get { return Resource.Console; }
        }

        public static string Price
        {
            get { return Resource.Price; }
        }


        #endregion

    }
}
