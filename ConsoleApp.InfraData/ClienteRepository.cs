using ConsoleApp.Domain;
using ConsoleApp.InfraData.Context;
using Dapper;
using Slapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.InfraData
{
    public class ClienteRepository : GetConnectionDapper, IClienteRepository
    {
        public virtual async Task<IEnumerable<Cliente>> ObtemClientes()
        {
            using (var conn = Connection)
            {
                var sql = @"SELECT A.Id, A.Nome, A.SobreNome, 
                            B.Id, Logradouro, B.Bairro, B.Cidade, B.Estado,
                            C.Id, C.Numero
                            FROM Cliente A
                            INNER JOIN Endereco B ON (A.Id = B.ClienteId)
                            INNER JOIN Telefone C ON (A.Id = C.ClienteId)";

                conn.Open();

                var clientes = new List<Cliente>();

                await conn.QueryAsync<Cliente, Endereco, Telefone, Cliente>(sql,
                    (cliente, endereco, telefone) =>
                    {
                        if(!clientes.Any(x => x.Id == cliente.Id))
                            clientes.Add(cliente);

                        if (clientes.Any(x => x.Id == cliente.Id))
                        {
                            var resultado = clientes.FirstOrDefault(x => x.Id == cliente.Id);
                            resultado.AdicionarEndereco(endereco);
                            resultado.AdicionarTelefone(telefone);
                        }

                        return cliente;
                    }, splitOn: "Id");

                return clientes;
            }
        }
    }

    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtemClientes();
    }
}
