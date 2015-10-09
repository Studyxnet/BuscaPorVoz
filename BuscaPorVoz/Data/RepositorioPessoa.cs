using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BuscaPorVoz
{
	public class RepositorioPessoa: RepositorioBase<Pessoa>, IRepositorioPessoa
	{
		public ICollection<Pessoa> GetPessoas ()
		{
			try {
				return base.RetornarTodos (typeof(Pessoa));
			} catch (Exception ex) {
				throw ex;
			}
		}

		public Pessoa GetPessoaPorId (int pessoaId)
		{
			try {
				return base.RetornarPorId (pessoaId);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public bool InserirPessoa (Pessoa pessoa)
		{
			try {
				return base.Inserir (pessoa);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public bool InserirPessoas (List<Pessoa> pessoas)
		{
			try {
				return base.InserirTodos (pessoas);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public bool RemoverPessoa (int pessoaId)
		{
			try {
				return base.Deletar (pessoaId);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public bool AtualizarPessoa (Pessoa pessoa)
		{
			try {
				return base.Atualizar (pessoa);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public bool ExistemPessoas ()
		{
			try {
				return base.ExisteRegistro ();
			} catch (Exception ex) {
				throw ex;
			}
		}

		public Pessoa GetPessoasPorTelefone (string telefone)
		{
			try {
				Expression<Func<Pessoa,bool>> porTelefone = (p) => p.Telefone == telefone;
				return base.ProcurarPorFiltro (porTelefone);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public Pessoa GetPessoasPorNome (string nome)
		{
			try {
				Expression<Func<Pessoa,bool>> porNome = (p) => p.Nome.StartsWith (nome) || p.Nome.EndsWith (nome);
				return base.ProcurarPorFiltro (porNome);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public Pessoa GetPessoasPorEmail (string email)
		{
			try {
				Expression<Func<Pessoa,bool>> porEmail = (p) => p.Email == email;
				return base.ProcurarPorFiltro (porEmail);
			} catch (Exception ex) {
				throw ex;
			}
		}

		public Pessoa GePessoaPorFiltro (Expression<Func<Pessoa, bool>> predicado)
		{
			try {
				return base.ProcurarPorFiltro (predicado);
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}

