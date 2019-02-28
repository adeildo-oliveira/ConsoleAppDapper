using ConsoleApp.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Interfaces.Services
{
    public interface IClienteService : IDisposable
    {
        Task InserirCliente(Cliente cliente);
    }
}
