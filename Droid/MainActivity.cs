using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Speech;
using Acr.UserDialogs;
using ImageCircle.Forms.Plugin.Droid;

namespace BuscaPorVoz.Droid
{
    [Activity(Label = "Busca Por Voz", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        , Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        private readonly int VOICE = 10;

        protected override void OnCreate(Bundle bundle)
        {
            UserDialogs.Init(this);
            ImageCircleRenderer.Init();
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultCode == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        App.fala = matches[0];
                        Xamarin.Forms.MessagingCenter.Send<BuscaPorVoz.SpeechPage>((BuscaPorVoz.SpeechPage)App._page, "achou");
                    }
                    else
                        Xamarin.Forms.MessagingCenter.Send<BuscaPorVoz.SpeechPage>((BuscaPorVoz.SpeechPage)App._page, "naoachou");
                }
                else
                    Xamarin.Forms.MessagingCenter.Send<BuscaPorVoz.SpeechPage>((BuscaPorVoz.SpeechPage)App._page, "naoachou");
            }
            else
                Xamarin.Forms.MessagingCenter.Send<BuscaPorVoz.SpeechPage>((BuscaPorVoz.SpeechPage)App._page, "cancelou");

            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}

