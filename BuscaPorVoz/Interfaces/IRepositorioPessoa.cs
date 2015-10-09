using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BuscaPorVoz
{
	public interface IRepositorioPessoa
	{
		ICollection<Pessoa> GetPessoas ();

		Pessoa GetPessoaPorId (int pessoaId);

		bool InserirPessoa (Pessoa pessoa);

		bool InserirPessoas (List<Pessoa> pessoas);

		bool RemoverPessoa (int pessoaId);

		bool AtualizarPessoa (Pessoa pessoa);

		bool ExistemPessoas ();

		Pessoa GetPessoasPorTelefone (string telefone);

		Pessoa GetPessoasPorNome (string nome);

		Pessoa GetPessoasPorEmail (string email);

		Pessoa GePessoaPorFiltro (Expression<Func<Pessoa, bool>> predicado);
	}
}

