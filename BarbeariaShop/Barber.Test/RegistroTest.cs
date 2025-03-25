using Baber.Model.Enum;
using Baber.Model.Request.Registros;
using Bogus;

namespace Barber.Test
{
    public class RegistroTest
    {
        public static Registros Teste()
        {
            var Falso = new Faker();

            var resposta = new Registros
            {
                Titulo = Falso.Commerce.Department(),
                Data = Falso.Date.Past(),
                Valor = Falso.Random.Decimal(min: 1, max: 1000),
                Pagamento = Falso.PickRandom<EnumRegistros>(),
                Descricao = Falso.Commerce.ProductDescription(),
            };

            return resposta;
            
        }
    }
}
