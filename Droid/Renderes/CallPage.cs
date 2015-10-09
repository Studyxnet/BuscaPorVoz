using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Views;
using Android.Content;

[assembly: ExportRenderer(typeof(BuscaPorVoz.CallPage), typeof(BuscaPorVoz.Droid.CallPage))]
namespace BuscaPorVoz.Droid
{
    public class CallPage : PageRenderer
    {
        Activity _activity;
        Android.Views.View view;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (e.OldElement != null || Element == null)
                    return;

                _activity = this.Context as Activity;

                var _speechView = _activity.LayoutInflater.Inflate(Resource.Layout.SpeechView, this, false);
                view = _speechView;

                this.FazLigacao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            view.Measure(msw, msh);
            view.Layout(0, 0, r - l, b - t);
        }

        private void FazLigacao()
        {
            var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", App.nroTelefoneParaLigar));
            var intent = new Intent(Intent.ActionDial, uri);
            _activity.StartActivity(intent);
            Xamarin.Forms.MessagingCenter.Send<BuscaPorVoz.CallPage>((BuscaPorVoz.CallPage)App._pageCall, "ligou");
        }
    }
}

