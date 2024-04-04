using diplom.Data;
using diplom.Models;
using diplom.Views;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom
{
    public partial class App : Application
    {

        private static DB diplomdatabase;
        public static DB Diplomdatabase
        {
            get
            {
                if (diplomdatabase == null)
                {
                    string destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Diplomdatabase.db3");
                    //File.Delete(destination);
                    diplomdatabase = new DB(destination);
                }
                return diplomdatabase;
                //создать контекстную бд в виде модели
            }
        }

        public static void DataBaseCopy()
        {
            string desktopPath = Path.Combine("storage", "self", "primary", "Android", "data", "com.companyname.diplom", "Diplomdatabase.db3"); //путь в доступную папку 
            string destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Diplomdatabase.db3");
            File.Copy(destination, desktopPath, true);
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Authorization());
        }

        protected override async void OnStart()
        {
            //await ContextData.SeedAsync(Diplomdatabase);
            //DataBaseCopy();
            List<User> users = await Diplomdatabase.GetUsersAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
