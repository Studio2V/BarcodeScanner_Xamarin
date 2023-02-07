using Android.Graphics.Drawables;
using BarcodeScanner.Droid.Renderer;
using BarcodeScanner.Views.BasePage;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationPageGradientHeader), typeof(NavigationPageGradientHeaderRenderer))]
namespace BarcodeScanner.Droid.Renderer
{
    [System.Obsolete]
    public class NavigationPageGradientHeaderRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            //run once when element is created
            if (e.OldElement != null || Element == null)
            {
                return;
            }

            var control = (NavigationPageGradientHeader)this.Element;
            var context = (MainActivity)this.Context;

           // context.ActionBar.SetBackgroundDrawable(new GradientDrawable(GradientDrawable.Orientation.RightLeft, new int[] { control.RightColor.ToAndroid(), control.LeftColor.ToAndroid() }));
        }
    }
}