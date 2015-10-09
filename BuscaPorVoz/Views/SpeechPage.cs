using System;

using Xamarin.Forms;
using Autofac;

namespace BuscaPorVoz
{
    public class SpeechPage : ContentPage
    {
        PessoaViewModel model;

        public SpeechPage()
        {
            this.BackgroundColor = Color.Black;

            Content = new StackLayout();

            MessagingCenter.Subscribe<SpeechPage>(this, "achou", (sender) =>
                {
                    this.model = App.container.Resolve<PessoaViewModel>();
                    this.model.GetPessoaPorNome_Voz(App.fala);
                });

            MessagingCenter.Subscribe<SpeechPage>(this, "naoachou", async (sender) =>
                {
                    this.model = App.container.Resolve<PessoaViewModel>();

                    var pagina = Activator.CreateInstance<ListaPessoasPage>();
                    await this.Navigation.PushModalAsync(pagina);
                });

            MessagingCenter.Subscribe<SpeechPage>(this, "cancelou", async (sender) =>
                {
                    this.model = App.container.Resolve<PessoaViewModel>();

                    var pagina = Activator.CreateInstance<ListaPessoasPage>();
                    await this.Navigation.PushModalAsync(pagina);
                });
        }
    }
}


