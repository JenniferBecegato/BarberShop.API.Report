using Baber.Model.Request.Registros;
using FluentValidation;

namespace Baber.Control.Filtro
{
    public class ValidatorRegistros : AbstractValidator<Registros>
    {
        public ValidatorRegistros()
        {
            RuleFor(request => request.Titulo).NotEmpty().WithMessage("O titulo não pode estar vazio");
            RuleFor(request => request.Valor).GreaterThan(0).WithMessage("O valor não pode estar vazio");
            RuleFor(request => request.Pagamento).IsInEnum().WithMessage("O tipo de pagamento precisa ser passado");
            RuleFor(request => request.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data não pode estar no futuro");
           
        }
        
    }
}
