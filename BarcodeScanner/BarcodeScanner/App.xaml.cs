using BarcodeScanner.Services;
using BarcodeScanner.Views;
using BarcodeScanner.Views.BasePage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeScanner
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new BasePage())
            {
                BarBackgroundColor = Color.FromHex("#0F2027")
            };
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
