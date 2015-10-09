using System;
using Xamarin.Forms;
using Android.Telephony.Gsm;

[assembly: Dependency(typeof(BuscaPorVoz.Droid.SendSMSRender_Droid))]
namespace BuscaPorVoz.Droid
{
    public class SendSMSRender_Droid : ISendSMS
    {
        public SendSMSRender_Droid()
        {
        }

        #region ISendSMS implementation

        public void SendSMS(string text, string telefone)
        {
            SmsManager.Default.SendTextMessage(telefone, null,
                text, null, null);
        }

        #endregion
    }
}

