using ConsoleApp.InfraData.Context;
using Dapper;

namespace Tests.Shared
{
    public abstract class DatabaseBuilder<T> : InMemoryBuilder<T> where T : class
    {
        protected virtual void CriarDependencias(string query) { }

        public T Criar(string query)
        {
            using (var conn = GetConnectionDapper.Connection)
            {
                conn.Open();
                conn.Query<T>(query);
            }

            return Instanciar();
        }
    }
}
