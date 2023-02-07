using BarcodeScanner.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Linq;
using System;
using BarcodeScanner.LocalDatabase;
using System.Windows.Input;
using BarcodeScanner.Views;
using BarcodeScanner.Services;

namespace BarcodeScanner.ViewModels
{
    public class ScanUtilsViewmodel: BaseViewModel
    {
        public delegate void scanEvent(ZXing.Result res);

        private int scanCount;

        ZXingScannerPage scanPage;

        // public event callAction;

        INavigation _navigation;

        public Command ScanCommand;
        public Command InitGetSavedCommand;
        public ICommand CopyTextCommand;

        public ICommand AboutDeveloperCommand;

        public ICommand LinkNavigationCommand;

        private ObservableCollection<ScanitemsList> scanitemsList;

        public ObservableCollection<ScanitemsList> ScanitemsList 
        {
            get => scanitemsList;
            set
            {
                scanitemsList = value;
                OnPropertyChanged();
            }
        }

        public int ScanCount 
        {
            get => scanCount;
            set 
            { 
                scanCount = value;
                OnPropertyChanged();
            } 
        }

        public ScanUtilsViewmodel(INavigation navigation):base()
        {
            _navigation = navigation;
            ScanCommand = new Command(async () =>await ScanBarcodesAsync());
            InitGetSavedCommand = new Command(async () =>await GetAllSavedBarcodes());
            CopyTextCommand = new Command<ScanitemsList>(CopyTextAsync);
            AboutDeveloperCommand = new Command(async()=>await AboutDeveloperPage());
            LinkNavigationCommand = new Command<ScanitemsList>(async(e)=> await LinkNavigationAction(e));
            ScanitemsList = new ObservableCollection<ScanitemsList>();
            InitGetSavedCommand.Execute(null);
        }

        public async Task GetAllSavedBarcodes()
        {
            database = await BarcodeDatabaseModel.Instance;
            var savedObjects = await database.GetItemsAsync();
            ScanitemsList = new ObservableCollection<ScanitemsList>(savedObjects);
            ScanCount = ScanitemsList?.Count ?? 0;
        }

        public void CopyTextAsync(ScanitemsList scanitemsList)
        {
            CopyToClipboardCommand.Execute(scanitemsList.LinkText);
        }

        public async Task LinkNavigationAction(ScanitemsList scanitemsList)
        {
            await DependencyService.Get<IIntentWrappers>().MakeIntentAsync(scanitemsList.LinkText);
        }

        public async Task AboutDeveloperPage()
        {
            await _navigation.PushAsync(new AboutPage());
        }

        public async Task ScanBarcodesAsync()
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += async (result) => {
                scanPage.IsScanning = false;
                var item = new ScanitemsList()
                {
                    LinkText = result.Text,
                    LinkType = result.BarcodeFormat.ToString(),
                    Scantime=DateTime.Now
                };
                ScanitemsList.Add(item);
                ScanCount = ScanitemsList?.Count ?? 0;
                await database.SaveItemAsync(item);
                Device.BeginInvokeOnMainThread(async () => {
                    await _navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("Scanned Barcode", result.Text + " , " + result.BarcodeFormat + " ," + result.ResultPoints[0].ToString(), "OK");
                });
            };
            await _navigation.PushModalAsync(scanPage);
        }

    }
}
