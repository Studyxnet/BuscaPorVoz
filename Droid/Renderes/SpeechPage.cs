using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BuscaPorVoz.SpeechPage), typeof(BuscaPorVoz.Droid.SpeechPage))]
namespace BuscaPorVoz.Droid
{
    public class SpeechPage : PageRenderer
    {
        private bool isRecording;
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

                if (_activity != null)
                {
                    if (!this.ValidaMicrofone())
                    {
                        var alert = new AlertDialog.Builder(_activity);
                        alert.SetTitle("Você não possui um microfone ou o mesmo não está habilitado");
                        alert.SetPositiveButton("OK", (sender, args) =>
                            {
                                return;
                            });
                        alert.Show();
                    }
                    else
                    {
                        App._page = (BuscaPorVoz.SpeechPage)e.NewElement;
                        this.CriarChamada(_activity);
                    }
                }
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

        private bool ValidaMicrofone()
        {
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
                return false;

            return true;
        }

        public void CriarChamada(Activity activity)
        {
            isRecording = !isRecording;
            if (isRecording)
            {
                // Cria o INTENT
                var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

                // Abre um modal com uma mensagem de voz
                voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Diga o nome da pessoa");

                // Se passar de 5.5s considera que não há mensagem falada
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 5500);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                // Para chamadas em outras líguas
                // voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
                // if you wish it to recognise the default Locale language and German
                // if you do use another locale, regional dialects may not be recognised very well

                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                activity.StartActivityForResult(voiceIntent, Constants.VOICE);
            }
        }
    }
}

