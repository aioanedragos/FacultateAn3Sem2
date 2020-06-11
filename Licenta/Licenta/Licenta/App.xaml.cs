using Licenta.Views;
using Xamarin.Forms;
using Licenta.Tables;
using System.IO;
using System;

namespace Licenta
{
    public partial class App : Application
    {
        static Databasechildren database;

        public static Databasechildren Database
        {
            get
            {
                if (database == null)
                {
                    database = new Databasechildren(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Children.db"));
                }
                return database;
            }
        }

        public static object DatabaseChildren { get; internal set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPAge());
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
