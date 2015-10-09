using System;

using Xamarin.Forms;
using Autofac;

namespace BuscaPorVoz
{
    public class IndexPage : ContentPage
    {
        Image splash;
        PessoaViewModel model;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.model = App.container.Resolve<PessoaViewModel>();
            this.model.ConfiguraNavigation(this.Navigation);
            this.model.CargaInicial();
        }

        public IndexPage()
        {
            this.splash = new Image
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("splash.jpg")),
                Aspect = Aspect.Fill
            };

            this.BackgroundColor = Color.Black;

            this.Content = new StackLayout
            {
                Children = { splash },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
        }
    }
}


