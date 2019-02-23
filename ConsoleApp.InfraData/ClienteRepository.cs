using ConsoleApp.Domain;
using ConsoleApp.InfraData.Context;
using ConsoleApp.InfraData.ScriptSQL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.InfraData
{
    public class ClienteRepository : GetConnectionDapper, IClienteRepository
    {
        public virtual async Task InserirCliente(Cliente cliente)
        {
            using (var conn = Connection)
            {
                conn.Open();
                await conn.QueryAsync(ClienteScripts.InserirCliente, new
                {
                    id = cliente.Id,
                    nome = cliente.Nome,
                    sobreNome = cliente.SobreNome
                }, commandType: CommandType.Text);
            }
        }

        public virtual async Task<Cliente> ObterCliente(Guid id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                return await conn.QueryFirstOrDefaultAsync<Cliente>(ClienteScripts.ObterCliente, new
                {
                    clienteId = id
                }, commandType: CommandType.Text);
            }
        }

        public virtual async Task<IEnumerable<Cliente>> ObterClientes()
        {
            using (var conn = Connection)
            {
                var clientes = new List<Cliente>();

                conn.Open();

                await conn.QueryAsync<Cliente, Endereco, Telefone, Cliente>(ClienteScripts.ObterClientes,
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
                    }, 
                    splitOn: "Id", 
                    commandType: CommandType.Text);

                return clientes;
            }
        }
    }

    public interface IClienteRepository
    {
        Task<Cliente> ObterCliente(Guid id);
        Task<IEnumerable<Cliente>> ObterClientes();
        Task InserirCliente(Cliente cliente);
    }
}
