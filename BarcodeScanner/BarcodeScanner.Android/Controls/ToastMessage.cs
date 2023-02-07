using Android.Widget;
using BarcodeScanner.Droid.Controls;
using BarcodeScanner.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace BarcodeScanner.Droid.Controls
{
    public class ToastMessage : IToast
    {
        public void showToast(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}