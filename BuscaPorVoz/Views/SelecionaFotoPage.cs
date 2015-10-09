using System;

using Xamarin.Forms;

namespace BuscaPorVoz
{
    public class SelecionaFotoPage : ContentPage
    {
        Image imgPerfil;
        StackLayout mainLayout;
        Button btnSelecionar;
        Button btnVoltar;

        public SelecionaFotoPage()
        {
            this.imgPerfil = new Image
            {
                WidthRequest = 100,
                HeightRequest = 100,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            this.btnSelecionar = new Button
            {
                BorderColor = AppColors.BrandColor(),
                BorderRadius = 6,
                BorderWidth = 0.5,
                Text = "Salvar",
                TextColor = Color.White,
                BackgroundColor = AppColors.BrandColor(),
                WidthRequest = 110,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            this.btnVoltar = new Button
            {
                BorderColor = AppColors.BrandColor(),
                BorderRadius = 6,
                BorderWidth = 0.5,
                Text = "Voltar",
                TextColor = Color.White,
                BackgroundColor = AppColors.BrandColor(),
                WidthRequest = 110,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            this.mainLayout = new StackLayout
            {
                Padding = new Thickness(8, 8, 8, 8),
                Children = { imgPerfil, btnSelecionar, btnVoltar }
            };

            this.Content = this.mainLayout;
        }

        private async void TrataClique()
        {
            if (this.imgPerfil != null && this.imgPerfil.Source != null)
            {
                var img = this.imgPerfil;

                MessagingCenter.Send<SelecionaFotoPage,Image>(this, "SalvouFoto", img);
                await this.Navigation.PopModalAsync();
            }
        }
    }
}


