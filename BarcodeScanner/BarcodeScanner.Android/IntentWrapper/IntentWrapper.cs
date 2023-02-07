using Android.App;
using Android.Content;
using BarcodeScanner.Common;
using BarcodeScanner.Services;
using Java.Lang;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(BarcodeScanner.Droid.IntentWrapper.IntentWrapper))]
namespace BarcodeScanner.Droid.IntentWrapper
{
    public class IntentWrapper : MainActivity,IIntentWrappers
    {
        [System.Obsolete]
        public async Task MakeIntentAsync(string intent)
        {
            await intentRoute(intent);
            
           // ((Activity)Forms.Context).StartActivity(reqintent);
        }

        private async Task intentRoute(string intent)
        {

            //switch()
            //return $"tel";
            await actionIntent(intent);
        }

        private async Task phoneIntent(string number)
        {
            Intent callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse($"{number}"));

            var status = await Permissions.CheckStatusAsync<Permissions.Phone>();
            if (status == PermissionStatus.Unknown || status == PermissionStatus.Denied)
            {
                await Permissions.RequestAsync<Permissions.Phone>();
            }
           // return callIntent;
        }    
        
        private async Task actionIntent(string text)
        {
            try
            {
                Intent browserIntent = new Intent(Intent.ActionView);
                browserIntent.SetData(Android.Net.Uri.Parse($"{text}"));

                ((Activity)Forms.Context).StartActivity(browserIntent);
            }
            catch(Exception e)
            {
                _ = e;
            }
            //if (browserIntent.ResolveActivity(((Activity)Forms.Context).PackageManager) != null)
            //{
            //    ((Activity)Forms.Context).StartActivity(browserIntent);
            //}
        }


    }
}