using System;
using Xamarin.Forms;

namespace BuscaPorVoz
{
    public class PessoaDetailsContent : ContentPage
    {
        public PessoaDetailsContent(string nome, string email, string descricao)
        {
            var _nome = new Label()
            {
                Text = nome,
                FontSize = 20,
                FontFamily = Device.OnPlatform("HelveticaNeue-Bold", "sans-serif-black", null),
                XAlign = TextAlignment.Center,
                TextColor = Color.Black
            };

            var _email = new Label()
            {
                Text = email,
                FontSize = 12,
                FontFamily = Device.OnPlatform("HelveticaNeue-Light", "sans-serif-light", null),
                XAlign = TextAlignment.Center,
                TextColor = Color.FromHex("#666")
            };

            var bio = new Label()
            {
                Text = descricao,
                FontSize = 14,
                FontFamily = Device.OnPlatform("HelveticaNeue", "sans-serif", null),
                XAlign = TextAlignment.Center,
                TextColor = Color.Black
            };

            var stack = new StackLayout()
            {
                Padding = new Thickness(20, 10),
                Children =
                {
                    _nome,
                    _email,
                    bio
                }
            };

            Content = stack;
        }
    }
}

