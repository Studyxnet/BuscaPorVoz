using System;
using Xamarin.Forms;

namespace BuscaPorVoz
{
    public class CallPage : ContentPage
    {
        public CallPage()
        {
            this.Content = new StackLayout();
            App._pageCall = this;

            MessagingCenter.Subscribe<CallPage>(this, "ligou", (sender) =>
                {
                    var _pagina = Activator.CreateInstance<ListaPessoasPage>();
                    this.Navigation.PushModalAsync(_pagina);
                });
        }
    }
}

