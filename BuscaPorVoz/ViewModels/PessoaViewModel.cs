using System;
using System.Linq;
using PropertyChanged;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Connectivity.Plugin;

namespace BuscaPorVoz
{
    [ImplementPropertyChanged]
    public class PessoaViewModel
    {
        public Pessoa Pessoa { get; protected set; }

        public ICollection<Pessoa> Pessoas { get; protected set; }

        public ICommand BtnBuscarPessoaPorVoz_Click { get; protected set; }

        public ICommand BtnAdicionarNovaPessoa_CLick { get; protected set; }

        public INavigation Navigation { get; protected set; }

        readonly IRepositorioPessoa repositorio;

        public void ConfiguraNavigation(INavigation navigation)
        {
            this.Navigation = navigation;
        }

        public PessoaViewModel(IRepositorioPessoa repositorio)
        {
            this.repositorio = repositorio;
            this.BtnBuscarPessoaPorVoz_Click = new Command(() =>
                {
                    this.AbrirPaginaDeReconhecimentoDeVoz();
                });
            this.BtnAdicionarNovaPessoa_CLick = new Command(() =>
                {
                    this.AdicionarNovaPessoa();
                });
        }

        public List<Pessoa> GetPessoas()
        {
            this.Pessoas = this.repositorio.GetPessoas();

            return this.Pessoas.ToList();
        }

        private async void AbrirPaginaDeReconhecimentoDeVoz()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var pagina = Activator.CreateInstance<SpeechPage>();
                App.Navigation = this.Navigation;
                await this.Navigation.PushModalAsync(pagina);
            }
            else
                Acr.UserDialogs.UserDialogs.Instance.Alert("Não há nenhuma conexão de internet ativa", "Aviso");
        }

        public bool InserirPessoa(Pessoa pessoa)
        {
            return this.repositorio.InserirPessoa(pessoa);
        }

        public bool RemoverPessoa(int pessoaId)
        {
            return this.repositorio.RemoverPessoa(pessoaId);
        }

        public Pessoa GetPessoa(int pessoaId)
        {
            return this.repositorio.GetPessoaPorId(pessoaId);
        }

        public void GetPessoaPorNome_Voz(string nome)
        {
            if (!String.IsNullOrEmpty(nome))
            {
                this.Pessoa = this.repositorio.GetPessoasPorNome(nome);

                if (this.Pessoa != null)
                {
                    var pagina = Activator.CreateInstance(typeof(PessoaDetalhePage), new[]{ this.Pessoa }) as PessoaDetalhePage;

                    if (this.Navigation == null)
                        this.Navigation = App.Navigation;

                    this.Navigation.PushModalAsync(pagina);
                }
                else
                {
                    var pagina = Activator.CreateInstance<ListaPessoasPage>();

                    if (this.Navigation == null)
                        this.Navigation = App.Navigation;

                    this.Navigation.PushModalAsync(pagina);
                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Nome não encontrado", "Aviso");
                            return false;
                        }
                    );
                }
            }
            else
            {
                var pagina = Activator.CreateInstance<ListaPessoasPage>();

                if (this.Navigation == null)
                    this.Navigation = App.Navigation;

                this.Navigation.PushModalAsync(pagina);
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Nome não reconhecido", "Aviso");
                        return false;
                    }
                );
            }
        }

        public void CargaInicial()
        {
            if (!this.repositorio.ExistemPessoas())
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Carregando", Acr.UserDialogs.MaskType.Clear);

                var rodrigo = new Pessoa
                {
                    Nome = "Rodrigo Amaro",
                    Distancia = 1.5m,
                    Email = "ramaral@icatuseguros.com.br",
                    NomeImagem = "rodrigo.jpg",
                    Offline = false,
                    Online = true,
                    Rating = 5.5m,
                    Telefone = "(21) 97551-9377",
                    Descricao = "Podemos já vislumbrar o modo pelo qual a estrutura atual da organização prepara-nos para enfrentar situações atípicas decorrentes do remanejamento dos quadros funcionais."
                };

                var luis = new Pessoa
                {
                    Nome = "Guilherme Rocha Rio",
                    Distancia = 35.5m,
                    Email = "lrio@icatuseguros.com.br",
                    NomeImagem = "luis.jpg",
                    Offline = false,
                    Online = true,
                    Rating = 7.5m,
                    Telefone = "(21) 97941-8822",
                    Descricao = "No entanto, não podemos esquecer que a constante divulgação das informações ainda não demonstrou convincentemente que vai participar na mudança dos métodos utilizados na avaliação de resultados."
                };

                var natasha = new Pessoa
                {
                    Nome = "Natasha Moura",
                    Distancia = 12.5m,
                    Email = "nbrasil@icatuseguros.com.br",
                    NomeImagem = "natasha.jpg",
                    Offline = true,
                    Online = false,
                    Rating = 9.5m,
                    Telefone = "(21) 98849-7283",
                    Descricao = "Não obstante, a adoção de políticas descentralizadoras agrega valor ao estabelecimento do investimento em reciclagem técnica."
                };

                var listaPessoas = new List<Pessoa>();
                listaPessoas.Add(rodrigo);
                listaPessoas.Add(luis);
                listaPessoas.Add(natasha);

                this.repositorio.InserirPessoas(listaPessoas);

                Acr.UserDialogs.UserDialogs.Instance.HideLoading();

                var pagina = Activator.CreateInstance<ListaPessoasPage>();
                this.Navigation.PushModalAsync(pagina);
            }
            else
            {
                var pagina = Activator.CreateInstance<ListaPessoasPage>();
                this.Navigation.PushModalAsync(pagina);
            }
        }

        public void AdicionarNovaPessoa()
        {
            var pagina = Activator.CreateInstance<NovaPessoaPage>();
            this.Navigation.PushModalAsync(pagina);
        }

        public bool SalvarNovaPessoa(Pessoa pessoa)
        {
            if (pessoa != null)
                return this.repositorio.InserirPessoa(pessoa);

            return false;
        }
    }
}

