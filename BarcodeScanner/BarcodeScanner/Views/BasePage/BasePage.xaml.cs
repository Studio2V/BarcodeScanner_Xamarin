using BarcodeScanner.Services;
using BarcodeScanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace BarcodeScanner.Views.BasePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : ContentPage
    {
        ScanUtilsViewmodel ScanUtilsViewmodel;
        public BasePage()
        {
            InitializeComponent();
            ScanUtilsViewmodel = new ScanUtilsViewmodel(Navigation);
            this.BindingContext = ScanUtilsViewmodel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScanUtilsViewmodel.scanResult += SendBarcodeInformation;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ScanUtilsViewmodel.scanResult -= SendBarcodeInformation;
        }

        private void SendBarcodeInformation(object e, Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //-= result;
                await Navigation.PopModalAsync();
                await DisplayAlert("Scanned Barcode", result.Text + " , " + result.BarcodeFormat + " ," + result.ResultPoints[0].ToString(), "OK");
            });
        }

        private async void scaned(object sender, System.EventArgs e)
        {
            ScanUtilsViewmodel.ScanCommand.Execute(e);
        }

        private void clipBoardImageClicked(object sender, EventArgs e)
        {
            var image = sender as Image;
            ScanUtilsViewmodel.CopyTextCommand.Execute(image.BindingContext);
        }

        private void AboutDeveloper(object sender, EventArgs e)
        {
            ScanUtilsViewmodel.AboutDeveloperCommand.Execute(null);
        }

        private void linkNavigationClicked(object sender, EventArgs e)
        {
            var image = sender as Frame;
            ScanUtilsViewmodel.LinkNavigationCommand.Execute(image.BindingContext);
        }
    }
}