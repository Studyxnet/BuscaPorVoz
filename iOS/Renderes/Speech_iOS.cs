using System;
using ApiAiSDK;
using ApiAi.iOS;
using ApiAi.Common;
using Xamarin.Forms.Platform.iOS;
using Newtonsoft.Json;
using ApiAiSDK.Model;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(BuscaPorVoz.SpeechPage), typeof(BuscaPorVoz.iOS.Speech_iOS))]
namespace BuscaPorVoz.iOS
{
    public class Speech_iOS : PageRenderer
    {
        AIConfiguration aiConfig;
        AIService aiService;

        public override void ViewDidLoad()
        {
            try
            {
                aiConfig = new AIConfiguration("135f1908-3d38-4954-83f4-d128948708b2",
                    "a8f248a646ad49d3aa8d5f3ee017009a",
                    SupportedLanguage.PortugueseBrazil);

                aiService = AIService.CreateService(aiConfig);

                this.StartListening();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            base.ViewDidLoad();
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        public Speech_iOS()
        {
           
        }

        public void StartListening()
        {
            InvokeInBackground(() => aiService.StartListening());
        }

        void AiService_OnResult(AIResponse response)
        {
            InvokeOnMainThread(() =>
                {
                    if (!response.IsError)
                    {
                        var jsonSettings = new JsonSerializerSettings
                        { 
                            NullValueHandling = NullValueHandling.Ignore
                        };

                        var responseString = JsonConvert.SerializeObject(response, Formatting.Indented, jsonSettings);
                        //resultTextView.Text = responseString;
                        DismissViewController(true, null);
                    }
                    else
                    {
                        //resultTextView.Text = response.Status.ErrorDetails;
                    }
                }
            );
        }

        void AiService_OnError(AIServiceException e)
        {
            Log.Debug("Speech_iOS_Renderer", e.ToString());

            InvokeOnMainThread(() =>
                {
                    //resultTextView.Text = e.ToString();
                }
            );
        }
    }
}

