namespace ConsoleApp.InfraData.ScriptSQL
{
    public class ClienteScripts
    {
        public const string InserirCliente = @"INSERT INTO Cliente VALUES (@id, @nome, @sobreNome)";

        public const string ObterCliente = @"exec sp_executesql 
        N'SET @IdCliente = @IdCliente;
            SELECT * FROM Cliente WHERE Id =  @IdCliente', 
        N'@IdCliente uniqueidentifier',  
        @IdCliente=@clienteId";

        public const string ObterClientes = @"SELECT A.Id, A.Nome, A.SobreNome, 
        B.Id, Logradouro, B.Bairro, B.Cidade, B.Estado,
        C.Id, C.Numero
        FROM Cliente A
        INNER JOIN Endereco B ON (A.Id = B.ClienteId)
        INNER JOIN Telefone C ON (A.Id = C.ClienteId)";
    }
}
