using System;

using Xamarin.Forms;
using Autofac;

namespace BuscaPorVoz
{
    public class App : Application
    {
        public static string fala;
        public static SpeechPage _page;
        public static CallPage _pageCall;
        public static SMSPage _pageSMS;
        public static string nroParaEnviarSMS;
        public static string textoParaEnviarSMS;
        public static IContainer container;
        public static INavigation Navigation;
        public static string nroTelefoneParaLigar;

        public App()
        {
            // The root page of your application
            MainPage = new IndexPage();

            // Para Testes
            //MainPage = new PessoaDetalhePage();

            container = AutofacConfiguration.Init();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

