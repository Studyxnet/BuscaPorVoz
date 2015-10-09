using System;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;

namespace BuscaPorVoz
{
    public class PessoaDetalhePage : ContentPage
    {
        Image backgroundImage;
        Image backgroundCurveEffect;
        Image messageImage;
        CircleImage fotoPessoa;
        Image callImage;
        Button btnVoltar;
        PessoaDetailsContent detailContent;
        Pessoa pessoa;

        public PessoaDetalhePage(Pessoa pessoa)
        {
            this.BackgroundColor = Color.White;
            this.pessoa = pessoa;

            this.backgroundImage = new Image
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("backgroudColors.jpg")),
                Aspect = Aspect.AspectFill,
                IsOpaque = true,
                Opacity = 0.8
            };

            if (Device.OS == TargetPlatform.Android)
            {
                this.backgroundCurveEffect = new Image
                {
                    Aspect = Aspect.AspectFill,
                    Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("dome_Android.png"))
                };
            }
            else if (Device.OS == TargetPlatform.iOS)
            {
                this.backgroundCurveEffect = new Image
                {
                    Aspect = Aspect.Fill,
                    Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("dome.png"))
                };
            }

            this.fotoPessoa = new CircleImage
            {
                Source = this.pessoa.ImagemSource,
                BorderColor = AppColors.BrandColor(),
                BorderThickness = 2,
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            this.messageImage = new Image
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("SMS-32.png"))
            };
            var messageClick = new TapGestureRecognizer();
            messageClick.Tapped += async (sender, e) =>
            {
                App.nroParaEnviarSMS = this.pessoa.Telefone;
                App.textoParaEnviarSMS = String.Format("Olá {0}", this.pessoa.Nome);

                var _pagina = Activator.CreateInstance<SMSPage>();
                await this.Navigation.PushModalAsync(_pagina);
            };
            this.messageImage.GestureRecognizers.Add(messageClick);

            this.callImage = new Image
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("Phone-32.png"))
            };
            var callClick = new TapGestureRecognizer();
            callClick.Tapped += async (sender, e) =>
            {
                App.nroTelefoneParaLigar = this.pessoa.Telefone;

                var _pagina = Activator.CreateInstance<CallPage>();
                await this.Navigation.PushModalAsync(_pagina);
            };
            this.callImage.GestureRecognizers.Add(callClick);

            this.detailContent = new PessoaDetailsContent(this.pessoa.Nome, this.pessoa.Email, this.pessoa.Descricao);

            this.btnVoltar = new Button
            {
                BorderColor = AppColors.BrandColor(),
                BorderRadius = 6,
                BorderWidth = 0.5,
                Text = "Voltar",
                TextColor = Color.White,
                BackgroundColor = AppColors.BrandColor(),
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 110
            };
            btnVoltar.Clicked += async (sender, e) =>
            {

                if (App.Navigation != null)
                {
                    var pagina = Activator.CreateInstance<ListaPessoasPage>();
                    await this.Navigation.PushModalAsync(pagina);
                }
                else
                    await this.Navigation.PopModalAsync();
            };

            var absLayout = new AbsoluteLayout{ HeightRequest = 100 };

            AbsoluteLayout.SetLayoutFlags(backgroundImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, 0.5));

            AbsoluteLayout.SetLayoutFlags(backgroundCurveEffect, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(backgroundCurveEffect, new Rectangle(0, 0.46, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            AbsoluteLayout.SetLayoutFlags(messageImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(messageImage, new Rectangle(0.15, 0.55, 35, 35));

            AbsoluteLayout.SetLayoutFlags(callImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(callImage, new Rectangle(0.85, 0.55, 30, 30));

            AbsoluteLayout.SetLayoutFlags(fotoPessoa, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fotoPessoa, new Rectangle(0.53, 0.40, 160, 160));

            AbsoluteLayout.SetLayoutFlags(detailContent.Content, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(detailContent.Content, new Rectangle(0.55, 0.80, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            AbsoluteLayout.SetLayoutFlags(btnVoltar, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(btnVoltar, new Rectangle(0.53, 0.95, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            absLayout.Children.Add(backgroundImage);
            absLayout.Children.Add(backgroundCurveEffect);
            absLayout.Children.Add(messageImage);
            absLayout.Children.Add(callImage);
            absLayout.Children.Add(fotoPessoa);
            absLayout.Children.Add(detailContent.Content);
            absLayout.Children.Add(btnVoltar);

            this.Content = absLayout;
        }
    }
}

