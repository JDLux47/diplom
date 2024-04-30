using diplom.Data;
using diplom.Models;
using diplom.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
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

        private static User loggedInUser;

        public static User LoggedInUser
        {
            get
            {
                if (loggedInUser != null)
                    return loggedInUser;
                else
                    return null;
            }

            set 
            { 
                loggedInUser = value; 
            }
        }

        public static void DataBaseCopy()
        {
            string desktopPath = Path.Combine("storage", "self", "primary", "Android", "data", "com.companyname.diplom", "Diplomdatabase.db3"); //путь в доступную папку 
            string destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Diplomdatabase.db3");
            File.Copy(destination, desktopPath, true);
        }

        public async void IsDatabaseExist()
        {
            await ContextData.SeedNeededInfoAsync(diplomdatabase);
        }

        public App()
        {
            InitializeComponent();

            string destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Diplomdatabase.db3");
            if (File.Exists(destination))
            {
                var logUser = SecureStorage.GetAsync("User").Result;

                if (logUser != null)
                {
                    LoggedInUser = Diplomdatabase.GetUserAsync(Convert.ToInt32(logUser)).Result;
                    MainPage = new AppShell();
                }
                else
                {
                    MainPage = new NavigationPage(new AuthorizationPage());
                }
            }
            else
            {
                // Создание новой базы данных и добавление данных
                diplomdatabase = new DB(destination);

                // Добавление данных в базу данных
                IsDatabaseExist();

                MainPage = new NavigationPage(new AuthorizationPage());
            }
        }

        protected override async void OnStart()
        {
            //await ContextData.SeedAsync(Diplomdatabase);
            //DataBaseCopy();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
