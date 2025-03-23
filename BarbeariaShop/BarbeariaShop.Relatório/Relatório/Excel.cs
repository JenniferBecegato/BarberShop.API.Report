using AutoMapper;
using Baber.Control.BarberDB;
using Baber.Model.Entity;
using Baber.Model.Enum;
using Barber.Report.Interface;
using ClosedXML.Excel;

namespace Barber.Report.Relatório
{
    public class Excel : IRelatório
    {
        private readonly string formato_moeda = "R$";

        public async Task<byte[]> Relatorio(int? id)
        {
            BarberShopDB Db = new BarberShopDB();
            List<Faturamento> profit;

            if (id != null)
                profit = Db.faturamento.Where(y => y.Id == id).ToList();
            else
                profit = Db.faturamento.ToList();

            if (profit.Count == 0 || profit == null)
            {
                return [];
            }
            var Excel = CriarExcel();
            var Abas = Aba(Excel);
            CriarCabecalho(Abas);

            int linha_atual = 2;
            foreach (var elementos in profit)
            {
                Abas.Cell($"A{linha_atual}").Value = elementos.Titulo;
                Abas.Cell($"B{linha_atual}").Value = elementos.Data.ToString("G");
                Abas.Cell($"C{linha_atual}").Value = TipoPagamentoTexto(elementos.pagamento);
                Abas.Cell($"D{linha_atual}").Style.NumberFormat.Format = $"{formato_moeda} #,##";
                Abas.Cell($"D{linha_atual}").Value = elementos.valor;
                Abas.Cell($"E{linha_atual}").Value = elementos.descricao;

                Abas.Cells($"A{linha_atual}:E{linha_atual}").Style.Font.FontColor = XLColor.Black;
                Abas.Cells($"A{linha_atual}:E{linha_atual}").Style.Fill.BackgroundColor = XLColor.White;
                linha_atual++;
            }

            var arquivo = new MemoryStream();
            Excel.SaveAs(arquivo);

            return arquivo.ToArray();
        }
        private XLWorkbook CriarExcel()
        {
            var excel = new XLWorkbook();
            excel.Author = "BarbeariaShop";
            excel.Style.Font.FontSize = 12;
            excel.Style.Font.FontName = "Times New Roman";

            return excel;
        }

        private IXLWorksheet Aba(XLWorkbook excel)
        {
            var aba = excel.Worksheets.Add(DateTime.Now.Month + "-" + DateTime.Now.Year);

            return aba;
        }

        private void CriarCabecalho(IXLWorksheet aba)
        {
            aba.Cell("A1").Value = "Título";
            aba.Cell("B1").Value = "Data";
            aba.Cell("C1").Value = "Pagamento";
            aba.Cell("D1").Value = "Valor";
            aba.Cell("E1").Value = "Descrição";

            aba.Cells("A1:E1").Style.Font.Bold = true;
            aba.Cells("A1:E1").Style.Font.FontColor = XLColor.White;

            aba.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#154447");
            aba.Cells("A1:E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }

        private string TipoPagamentoTexto(EnumRegistros tipo)
        {
            return tipo switch
            {
             EnumRegistros.Dinheiro => EnumRegistros.Dinheiro.ToString(),
             EnumRegistros.CartãoDebito => EnumRegistros.CartãoDebito.ToString(),
             EnumRegistros.CartãoCredito => EnumRegistros.CartãoCredito.ToString(),
             EnumRegistros.Pix => EnumRegistros.Pix.ToString(),
                _ => String.Empty
            };
           
        }
    }
}
