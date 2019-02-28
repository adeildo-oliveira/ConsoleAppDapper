using ConsoleApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Interfaces.Repository
{
    public interface IClienteRepository : IDisposable
    {
        Task<Cliente> ObterCliente(Guid id);
        Task<IEnumerable<Cliente>> ObterClientes();
        Task InserirCliente(Cliente cliente);
    }
}
