using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(BuscaPorVoz.BioEntry), typeof(BuscaPorVoz.Droid.BioEntryRender))]
namespace BuscaPorVoz.Droid
{
    public class BioEntryRender : EditorRenderer
    {
        public BioEntryRender()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            if (e.OldElement != null || this.Element == null)
                return;

            //e.NewElement.Text = "Bio";
        }
    }
}

