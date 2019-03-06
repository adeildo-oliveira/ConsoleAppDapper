using FluentValidation.Results;
using System.Collections.Generic;

namespace Tests.Shared
{
    public class ValidationResultBuilder : InMemoryBuilder<ValidationResult>
    {
        private string _propriedade;
        private string _mensagem;

        public ValidationResultBuilder ComPropriedade(string propriedade)
        {
            _propriedade = propriedade;
            return this;
        }

        public ValidationResultBuilder ComMensagem(string mensagem)
        {
            _mensagem = mensagem;
            return this;
        }

        public override ValidationResult Instanciar()
        {
            return new ValidationResult(
                new List<ValidationFailure>
                {
                    new ValidationFailure(_propriedade, _mensagem)
                });
        }
    }
}
