using System;
using Autofac;

namespace BuscaPorVoz
{
    public static class AutofacConfiguration
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();

            // Registrando Serviços e dependências.
            //builder.RegisterInstance(new PessoaService()).As<IPessoaService>();

            // Registrando IoC para Repositório de Dados
            builder.RegisterInstance(new RepositorioPessoa()).As<IRepositorioPessoa>();

            // Registrando ViewModels.
            builder.RegisterType<PessoaViewModel>();

            return builder.Build();
        }
    }
}

