using EVTovar.Services;
using EVTovar.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EVTovar
{
    public partial class App : Application
    {
        static IDataService dataService;

        public static IDataService DataService
        {
            get
            {
                if (dataService == null)
                {
                    dataService = new DataService();
                }
                return dataService;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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
