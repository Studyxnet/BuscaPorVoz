using System;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;

namespace BuscaPorVoz
{
    public class PessoaViewCell : ViewCell
    {
        public PessoaViewCell()
        {
            var pessoaProfileImage = new CircleImage
            {
                BorderColor = AppColors.BrandColor(),
                BorderThickness = 2,
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            pessoaProfileImage.SetBinding(Image.SourceProperty, "ImagemSource");

            var distanciaLabel = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                TextColor = Color.FromHex("#666")
            };
            distanciaLabel.SetBinding(
                Label.TextProperty, new Binding(
                    "Distancia", 
                    stringFormat: "{0} Kilometros ")
            );

            var nomeLabel = new Label()
            {
                FontFamily = Device.OnPlatform("HelveticaNeue-Bold", "sans-serif-black", null),
                FontSize = 18,
                TextColor = Color.Black
            };
            nomeLabel.SetBinding(Label.TextProperty, "Nome");

            var onlineImage = new Image()
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("online.png")),
                HeightRequest = 8,
                WidthRequest = 8
            };
            onlineImage.SetBinding(Image.IsVisibleProperty, "Online");
            var onLineLabel = new Label()
            { 
                FontFamily = Device.OnPlatform("HelveticaNeue-Bold", "sans-serif-black", null),
                Text = "Online", 
                TextColor = AppColors.BrandColor(),
                FontSize = 12,
            };
            onLineLabel.SetBinding(Label.IsVisibleProperty, "Online");

            var offlineImage = new Image()
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("offline.png")),
                HeightRequest = 8,
                WidthRequest = 8
            };
            offlineImage.SetBinding(Image.IsVisibleProperty, "Offline");
            var offLineLabel = new Label()
            { 
                FontFamily = Device.OnPlatform("HelveticaNeue-Bold", "sans-serif-black", null),
                Text = "5 horas atrás", 
                TextColor = Color.FromHex("#ddd"),
                FontSize = 12,
            };
            offLineLabel.SetBinding(Label.IsVisibleProperty, "Offline");

            var starLabel = new Label()
            { 
                FontFamily = Device.OnPlatform("HelveticaNeue-Bold", "sans-serif-black", null),
                FontSize = 12,
                TextColor = Color.Gray
            };
            starLabel.SetBinding(Label.TextProperty, "Rating");

            var starImage = new Image()
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("star.png")),
                HeightRequest = 12,
                WidthRequest = 12
            };

            var ratingStack = new StackLayout()
            {
                Spacing = 3,
                Orientation = StackOrientation.Horizontal,
                Children = { starImage, starLabel }
            };

            var statusLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                { 
                    distanciaLabel, 
                    onlineImage, 
                    onLineLabel, 
                    offlineImage, 
                    offLineLabel 
                }
            };

            var vetDetailsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nomeLabel, statusLayout, ratingStack }
            };

            var tapImage = new Image()
            {
                Source = ImageSource.FromResource(Constants.RetornarCaminhoParaImagens("tap.png")),
                HorizontalOptions = LayoutOptions.End,
                HeightRequest = 12,
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { pessoaProfileImage, vetDetailsLayout, tapImage }
            };

            this.View = cellLayout;
        }
    }
}

