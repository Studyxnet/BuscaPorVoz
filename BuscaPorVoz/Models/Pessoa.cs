using System;
using Xamarin.Forms;
using SQLite.Net.Attributes;

namespace BuscaPorVoz
{
    public class Pessoa
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string NomeImagem { get; set; }

        public bool Online { get; set; }

        public bool Offline { get; set; }

        public decimal Distancia { get; set; }

        public decimal Rating { get; set; }

        public string Descricao { get; set; }

        [Ignore]
        public ImageSource ImagemSource
        {
            get
            {
                if (!String.IsNullOrEmpty(this.NomeImagem))
                {
                    return ImageSource.FromResource(Constants.RetornarCaminhoParaImagens(this.NomeImagem));
                }

                return null;
            }
        }
    }
}

