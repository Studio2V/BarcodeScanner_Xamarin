using BarcodeScanner.LocalDatabase;
using BarcodeScanner.Models;
using BarcodeScanner.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BarcodeScanner.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public BarcodeDatabaseModel database;

        public event EventHandler<ZXing.Result> scanResult;
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public Command loadDBInstance => new Command(async () => await LoadInstaces());

        public Command CopyToClipboardCommand => new Command<string>(async (e) => await copyToClipboard(e));

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public BaseViewModel()
        {
            loadDBInstance.Execute(null);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void scanResultInvoke(ZXing.Result param)
        {
            scanResult?.Invoke(this,param);
        }

        private async Task copyToClipboard(string text)
        {
            await Clipboard.SetTextAsync(text);
            DependencyService.Get<IToast>().showToast("Barcode Text Copied");
        }

        private async Task LoadInstaces()
        {
           // database = await BarcodeDatabaseModel.Instance;
        }
        #endregion
    }
}
