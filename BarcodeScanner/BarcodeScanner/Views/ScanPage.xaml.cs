using BarcodeScanner.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace BarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        ScanUtilsViewmodel ScanUtilsViewmodel;

        public ScanPage()
        {
            InitializeComponent();
            this.BindingContext = ScanUtilsViewmodel=new ScanUtilsViewmodel(Navigation);
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

        private void SendBarcodeInformation(object e,Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //-= result;
                // await Navigation.PopModalAsync();
                await DisplayAlert("Scanned Barcode", result.Text + " , " + result.BarcodeFormat + " ," + result.ResultPoints[0].ToString(), "OK");
            });
        }

        private void scaned(object sender, System.EventArgs e)
        {
            ScanUtilsViewmodel.ScanBarcodesAsync();
        }

        private void scanned(object sender, EventArgs e)
        {

        }
    }
}