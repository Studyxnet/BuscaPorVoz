using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BuscaPorVoz
{
    public abstract class RepositorioBase<T>: IRepositorioBase<T> where T : class, new()
    {
        SQLiteConnection connection { get; set; }

        private object _lock = new object();

        public RepositorioBase()
        {
            this.connection = DependencyService.Get<ISQLite>().GetConnection();

            this.CriarBase();
        }

        protected void CriarBase()
        {
            try
            {
                this.connection.BeginTransaction();
                this.connection.CreateTable<Pessoa>();
                this.connection.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IRepositorioBase implementation

        public bool Inserir(T entidade)
        {
            lock (_lock)
                return this.connection.InsertOrReplace(entidade) > 0;
        }

        public bool InserirTodos(ICollection<T> listaEntidade)
        {
            lock (_lock)
                return this.connection.InsertOrReplaceAll(listaEntidade) > 0;
        }

        public bool Deletar(int entidadeId)
        {
            lock (_lock)
                return this.connection.Delete(entidadeId) > 0;
        }

        public ICollection<T> ProcurarPorColecao(Type tipoObjeto, EnumTipoQuery query, object param)
        {
            var result = new List<T>();

            lock (_lock)
            {
                if (tipoObjeto == typeof(Pessoa))
                {
                    switch (query)
                    {
                        case EnumTipoQuery.SELECT_PESSOA_POR_EMAIL:
                            result = this.connection.Query<T>(String.Concat(Constants.QUERY_PESSOA_POR_EMAIL, Convert.ToString(param)));
                            break;

                        case EnumTipoQuery.SELECT_PESSOA_POR_NOME:
                            result = this.connection.Query<T>(String.Concat(Constants.QUERY_PESSOA_POR_NOME, Convert.ToString(param)));
                            break;

                        case EnumTipoQuery.SELECT_PESSOA_POR_TELEFONE:
                            result = this.connection.Query<T>(String.Concat(Constants.QUERY_PESSOA_POR_TELEFONE, Convert.ToString(param)));
                            break;
                    }
                }
            }

            return result;
        }

        public T ProcurarPorFiltro(Expression<Func<T, bool>> filtro)
        {
            lock (_lock)
                return this.connection.Table<T>().Where(filtro).FirstOrDefault();
        }

        public ICollection<T> RetornarTodos(Type tipoObjeto)
        {
            lock (_lock)
            {
                if (tipoObjeto == typeof(Pessoa))
                    return this.connection.Query<T>(Constants.QUERY_PESSOA_TODAS);
            }

            return null;
        }

        public T RetornarPorId(int pkId)
        {
            lock (_lock)
                return this.connection.Get<T>(pkId);
        }

        public bool Atualizar(T entidade)
        {
            lock (_lock)
                return this.connection.Update(entidade) > 0;
        }

        public bool ExisteRegistro()
        {
            lock (_lock)
                return this.connection.Table<T>().Count() > 0;
        }

        #endregion
    }
}

