using Baber.Model.Enum;

namespace Baber.Model.Entity
{
    public class Faturamento
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public DateTime Data { get; set; }
        public EnumRegistros pagamento { get; set; }
        public decimal valor { get; set; }
        public string descricao { get; set; } = "";
    }
}
