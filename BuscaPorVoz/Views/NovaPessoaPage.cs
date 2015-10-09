using System;
using Xamarin.Forms;
using Autofac;
using ImageCircle.Forms.Plugin.Abstractions;
using System.Threading.Tasks;

namespace BuscaPorVoz
{
    public class NovaPessoaPage : ContentPage
    {
        Entry txtNome;
        Entry txtEmail;
        Entry txtTelefone;
        BioEntry txtBio;
        Pessoa pessoa;
        Image imgFoto;
        CircleImage unknownImage;
        Button btnAdicionarPessoa;
        Button btnVoltar;
        StackLayout mainlayout;
        PessoaViewModel model;

        protected override void OnAppearing()
        {
            this.model = App.container.Resolve<PessoaViewModel>();
            base.OnAppearing();
        }

        public NovaPessoaPage()
        {
            txtNome = new Entry
            {
                Placeholder = "Nome",
                HeightRequest = 60,
                WidthRequest = 80
            };

            txtEmail = new Entry
            {
                Placeholder = "Email",
                HeightRequest = 60,
                WidthRequest = 80
            };

            txtTelefone = new Entry
            {
                Placeholder = "Telefone",
                HeightRequest = 60,
                WidthRequest = 80
            };

            txtBio = new BioEntry
            {
                HeightRequest = 150,
                WidthRequest = 150,
            };

            unknownImage = new CircleImage
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("Unknown.gif"))
            };
            var unknownImage_Click = new TapGestureRecognizer();
            unknownImage_Click.Tapped += async (sender, e) => await TrataCliqueNaFoto();
            this.unknownImage.GestureRecognizers.Add(unknownImage_Click);

            btnAdicionarPessoa = new Button
            {
                BorderColor = AppColors.BrandColor(),
                BorderRadius = 6,
                BorderWidth = 0.5,
                Text = "Adicionar",
                TextColor = Color.White,
                BackgroundColor = AppColors.BrandColor(),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 110,
                HeightRequest = 40
            };
            this.btnAdicionarPessoa.Clicked += (sender, e) => TrataClique();

            this.btnVoltar = new Button
            {
                BorderColor = AppColors.BrandColor(),
                BorderRadius = 6,
                BorderWidth = 0.5,
                Text = "Voltar",
                TextColor = Color.White,
                BackgroundColor = AppColors.BrandColor(),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 110,
                HeightRequest = 40
            };

            var buttonsLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    btnAdicionarPessoa,
                    btnVoltar
                }
            };

            mainlayout = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(10, 5, 0), 5, 0),
                Children =
                {
                    unknownImage,
                    txtNome,
                    txtEmail,
                    txtTelefone,
                    txtBio,
                    buttonsLayout
                }
            };

            MessagingCenter.Subscribe<SelecionaFotoPage,Image>(this, "SalvouFoto", (sender, arg) =>
                {
                    if (arg != null && arg.Source != null)
                    {
                        this.imgFoto = new Image();
                        this.imgFoto = arg;
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Imagem salva com sucesso");
                    }
                });

            this.Content = mainlayout;
        }

        private void TrataClique()
        {
            this.pessoa = new Pessoa
            {
                Nome = this.txtNome.Text,
                Email = this.txtEmail.Text,
                Descricao = this.txtBio.Text,
                Distancia = Convert.ToDecimal(new Random().Next()),
                Offline = true,
                Online = false,
                Rating = Convert.ToDecimal(new Random().Next()),
                Telefone = this.txtTelefone.Text,
                NomeImagem = "Unknow.gif"
            };

            if (this.model.SalvarNovaPessoa(pessoa))
            {
                var pagina = Activator.CreateInstance<ListaPessoasPage>();
                this.Navigation.PushModalAsync(pagina);

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Pessoa salva com sucesso");
                        return false;
                    });
            }
            else
                Acr.UserDialogs.UserDialogs.Instance.Alert("Erro ao salvar pessoa");
        }

        private async Task TrataCliqueNaFoto()
        {
            var _pagina = Activator.CreateInstance<SelecionaFotoPage>();
            await this.Navigation.PushModalAsync(_pagina);
        }
    }
}

