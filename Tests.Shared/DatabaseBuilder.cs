using ConsoleApp.InfraData.Context;
using Dapper;

namespace Tests.Shared
{
    public abstract class DatabaseBuilder<T> : InMemoryBuilderDataBase<T> where T : class
    {
        protected virtual void CriarDependencias(string query) => ExecutarScript(query);

        protected virtual T Criar(string query)
        {
            ExecutarScript(query);

            return Instanciar();
        }

        private static void ExecutarScript(string query)
        {
            using (var conn = GetConnectionDapper.Connection)
            {
                conn.Open();
                conn.Query<T>(query);
            }
        }
    }
}
