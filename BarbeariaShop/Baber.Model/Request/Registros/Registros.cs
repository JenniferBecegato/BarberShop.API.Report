using Baber.Model.Enum;

namespace Baber.Model.Request.Registros
{
    public class Registros
    {
        public string Titulo { get; set; } = "";
        public DateTime Data { get; set; }
        public EnumRegistros Pagamento { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = "";
    }
}
