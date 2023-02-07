using BarcodeScanner.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BarcodeScanner.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}