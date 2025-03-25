using Baber.Control.Filtro;
using Baber.Model.Enum;

namespace Barber.Test
{
    public class Test
    {
        [Fact]
        public void Sucesso()
        {
            var Validacao = new ValidatorRegistros();
            var Validacao2 = RegistroTest.Teste();

            var Resposta = Validacao.Validate(Validacao2);

            Assert.True(Resposta.IsValid);

        }
        [Fact]
        public void Erro_titulo()
        {
            var Validacao = new ValidatorRegistros();
            var Validacao2 = RegistroTest.Teste();
            Validacao2.Titulo = "";

            var Resposta = Validacao.Validate(Validacao2);

            Assert.False(Resposta.IsValid);
            Assert.True(Resposta.Errors.Where(erro => erro.ErrorMessage.Equals("O titulo não pode estar vazio")).ToList().Count == 1);
        }
        [Fact]
        public void Erro_Data()
        {
            var Validacao = new ValidatorRegistros();
            var Validacao2 = RegistroTest.Teste();
            Validacao2.Data = DateTime.Now.AddDays(1);

            var Resposta = Validacao.Validate(Validacao2);

            Assert.False(Resposta.IsValid);
            Assert.True(Resposta.Errors.Where(erro => erro.ErrorMessage.Equals("A data não pode estar no futuro")).ToList().Count == 1);
        }
        [Fact]
        public void Erro_Pagamento()
        {
            var Validacao = new ValidatorRegistros();
            var Validacao2 =  RegistroTest.Teste();
            Validacao2.Pagamento = (EnumRegistros)700 ;

            var Resposta = Validacao.Validate(Validacao2);

            Assert.False(Resposta.IsValid);
            Assert.True(Resposta.Errors.Where(erro => erro.ErrorMessage.Equals("O tipo de pagamento precisa ser passado")).ToList().Count == 1);
        }
        [Fact]
        public void Erro_Valor()
        {
            var Validacao = new ValidatorRegistros();
            var Validacao2 =  RegistroTest.Teste();
            Validacao2.Valor = 0;

            var Resposta = Validacao.Validate(Validacao2);

            Assert.False(Resposta.IsValid);
            Assert.True(Resposta.Errors.Where(erro => erro.ErrorMessage.Equals("O valor não pode estar vazio")).ToList().Count == 1);
        }

    }
}