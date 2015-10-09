using System;

using Xamarin.Forms;

namespace BuscaPorVoz
{
    public class SMSPage : ContentPage
    {
        public SMSPage()
        {
            Content = new StackLayout();
            App._pageSMS = this;

            MessagingCenter.Subscribe<SMSPage>(this, "sms", (sender) =>
                {
                    var _pagina = Activator.CreateInstance<ListaPessoasPage>();
                    this.Navigation.PushModalAsync(_pagina);
                });
        }
    }
}


