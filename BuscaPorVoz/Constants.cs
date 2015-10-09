using System;

namespace BuscaPorVoz
{
    public static class Constants
    {
        public static string QUERY_PESSOA_POR_EMAIL = "SELECT * FROM PESSOA WHERE Email = ";
        public static string QUERY_PESSOA_POR_NOME = "SELECT * FROM PESSOA WHERE Nome = ";
        public static string QUERY_PESSOA_POR_TELEFONE = "SELECT * FROM PESSOA WHERE Telefone = ";
        public static string QUERY_PESSOA_TODAS = "SELECT * FROM PESSOA";

        public static string RetornarCaminhoParaImagens(string arquivoNome)
        {
            return String.Format("BuscaPorVoz.Content.Images.{0}", arquivoNome);
        }
    }
}

