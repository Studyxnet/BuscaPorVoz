using System;
using System.Linq;

using Xamarin.Forms;
using Autofac;
using System.Collections;
using System.Threading.Tasks;

namespace BuscaPorVoz
{
    public class ListaPessoasPage : ContentPage
    {
        private PessoaViewModel model;
        ListView listViewPessoas;
        Button btnBuscarPessoaPorVoz;
        Button btnAdicionarPessoa;
        StackLayout mainlayout;

        protected override void OnAppearing()
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Carregando", Acr.UserDialogs.MaskType.Black);

            this.BindingContext = model = App.container.Resolve<PessoaViewModel>();
            this.model.ConfiguraNavigation(this.Navigation);

            this.listViewPessoas.ItemsSource = model.GetPessoas();

            Acr.UserDialogs.UserDialogs.Instance.HideLoading();

            base.OnAppearing();
        }

        public ListaPessoasPage()
        {
            this.listViewPessoas = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(PessoaViewCell))
            };

            this.listViewPessoas.ItemTapped += async (sender, e) => await TrataCliqueNoListView(e.Item);
            this.listViewPessoas.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return; 
                }

                ((ListView)sender).SelectedItem = null; 
            };

            this.btnBuscarPessoaPorVoz = new Button
            {
                BorderColor = AppColors.BrandColor(),
                BorderRadius = 6,
                BorderWidth = 0.5,
                Text = "Buscar",
                TextColor = Color.White,
                BackgroundColor = AppColors.BrandColor(),
                WidthRequest = 110,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            this.btnBuscarPessoaPorVoz.SetBinding(Button.CommandProperty, "BtnBuscarPessoaPorVoz_Click");

            this.btnAdicionarPessoa = new Button
            {
                BorderColor = AppColors.BrandColor(),
                BorderRadius = 6,
                BorderWidth = 0.5,
                Text = "Adicionar",
                TextColor = Color.White,
                BackgroundColor = AppColors.BrandColor(),
                WidthRequest = 110,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            this.btnAdicionarPessoa.SetBinding(Button.CommandProperty, "BtnAdicionarNovaPessoa_CLick");

            var btnFooter = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 0, 10, 5),
                Children = { btnBuscarPessoaPorVoz, btnAdicionarPessoa }
            };

            this.mainlayout = new StackLayout
            {
                Padding = new Thickness(5, Device.OnPlatform(20, 10, 0), 5, 5),
                Children =
                {
                    listViewPessoas,
                    btnFooter
                }
            };

            this.Content = mainlayout;
        }

        private async Task TrataCliqueNoListView(object item)
        {
            var pessoaSelecionada = (Pessoa)item;

            if (pessoaSelecionada != null)
            {
                var pagina = Activator.CreateInstance(typeof(PessoaDetalhePage), new[]{ pessoaSelecionada }) as PessoaDetalhePage;
                await this.Navigation.PushModalAsync(pagina);
            }
            else
            {
                var alertConfig = new Acr.UserDialogs.AlertConfig();
                alertConfig.Message = "Houve um erro ao selecionar esta pessoa";
                alertConfig.OkText = "Continuar";
                alertConfig.Title = "Erro";

                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(alertConfig);
            }
        }
    }
}


