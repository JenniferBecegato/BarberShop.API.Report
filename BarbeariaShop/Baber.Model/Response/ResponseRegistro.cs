using Baber.Model.Enum;

namespace Baber.Model.Response
{
    public class ResponseRegistro
    {
        public string Titulo { get; set; } = "";
        public EnumRegistros Pagamento { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = "";
    }
}
