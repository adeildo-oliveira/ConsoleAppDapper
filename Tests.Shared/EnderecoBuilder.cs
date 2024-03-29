﻿using ConsoleApp.Domain.Models;
using System;

namespace Tests.Shared
{
    public class EnderecoBuilder : DatabaseBuilder<Endereco>
    {
        private Guid _id;
        private string _logradouro;
        private string _bairro;
        private string _cidade;
        private string _estado;
        private Cliente _cliente;

        public EnderecoBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        public EnderecoBuilder ComLogradouro(string logradouro)
        {
            _logradouro = logradouro;
            return this;
        }

        public EnderecoBuilder ComBairro(string bairro)
        {
            _bairro = bairro;
            return this;
        }

        public EnderecoBuilder ComCidade(string cidade)
        {
            _cidade = cidade;
            return this;
        }

        public EnderecoBuilder ComEstado(string estado)
        {
            _estado = estado;
            return this;
        }

        public EnderecoBuilder ComCliente(Cliente cliente)
        {
            _cliente = cliente;
            return this;
        }

        public override Endereco Instanciar() => new Endereco(_id, _logradouro, _bairro, _cidade, _estado);

        public override Endereco Criar() => 
            Criar($"insert into Endereco values ('{_id}', '{_cliente.Id}', '{_logradouro}', '{_bairro}', '{_cidade}', '{_estado}')");
    }
}
