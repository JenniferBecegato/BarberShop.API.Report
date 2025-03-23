using AutoMapper;
using Baber.Control.BarberDB;
using Baber.Model.Entity;
using Baber.Model.Enum;
using Barber.Report.Interface;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Reflection;

namespace Barber.Realatório.Pdf
{
    public class Pdf : IRelatório
    {
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

            var documento = CriarDocumento();
            var pagina = CriarPage(documento);


            
          

            // Caminho da imagem (caminho relativo)


            Paragraph paragraph;
            paragraph = pagina.AddParagraph();
            
            //Cabeçalho
            string texto = string.Format("Faturamento da semana");
            paragraph.AddFormattedText(texto, TextFormat.Bold);
            float tamanhoFonte = 14;
            paragraph.Format.Font.Size = tamanhoFonte;

            //SubTotal faturamento
            paragraph.AddFormattedText("\nTotal: " + TotalFaturamento(profit) + " reais");
            paragraph.AddFormattedText("\n");

            string imagePath = @"C:\Users\Jenni\Documents\Rocktseat\BarbeariaShop\BarbeariaShop.Relatório\Imagem\logoBarberShop.png";
            // Adiciona a imagem ao documento
            Image image = pagina.AddImage(imagePath);

            // Ajusta a largura e altura da imagem, se necessário
            image.Top = Unit.FromCentimeter(0);
            image.Left = Unit.FromCentimeter(15);
            image.LockAspectRatio = true;  // Para manter a proporção
            image.Width = Unit.FromCentimeter(2); // Largura de 2 cm                                     

           
            var table = pagina.AddTable();
            //Colunas inteligentes
            Type TabelaFaturamento = typeof(Faturamento);
            PropertyInfo[] propriedades = TabelaFaturamento.GetProperties();
            int quantidadePropriedades = propriedades.Length;
            for (int i = 0; i < quantidadePropriedades; i++)
            {
                table.AddColumn();
            }

            //Centralização das palavras dentro da tabela
            table.Format.Alignment = ParagraphAlignment.Center;

            foreach (Faturamento faturamento in profit)
            {
                var row = table.AddRow();
                row.Cells[0].AddParagraph("Título");
                row.Cells[1].AddParagraph("Data");
                row.Cells[2].AddParagraph("Pagamento");
                row.Cells[3].AddParagraph("Valor");
                row.Cells[4].AddParagraph("Descrição");

                //Estilização da tabela (negrito)
                row.Cells[0].Format.Font.Bold = true;
                row.Cells[1].Format.Font.Bold = true;
                row.Cells[2].Format.Font.Bold = true;
                row.Cells[3].Format.Font.Bold = true;
                row.Cells[4].Format.Font.Bold = true;

                // Altere a cor de fundo das células do cabeçalho
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[3].Shading.Color = Colors.LightGray;
                row.Cells[4].Shading.Color = Colors.LightGray;

                row = table.AddRow();
                row.Cells[0].AddParagraph(faturamento.Titulo);
                row.Cells[1].AddParagraph(faturamento.Data.ToString("dd/MM/yyyy"));
                row.Cells[2].AddParagraph(PagamentoTexto(faturamento.pagamento));
                row.Cells[3].AddParagraph(faturamento.valor + " reais");
                row.Cells[4].AddParagraph(faturamento.descricao);
                row = table.AddRow();

            }

            //Linhas na tabela 
            foreach (Row r in table.Rows)
            {
                foreach (Cell cell in r.Cells)
                {
                    
                    cell.Borders.Width = 0.75;
                    cell.Borders.Color = Colors.Black;
                }
            }

            return RenderDocumento(documento);
        }

        private Document CriarDocumento()
        {
            var document = new Document();
            document.Info.Title = "Faturamento";
            document.Info.Author = "BarbeariaShop";

            return document;
        }

        private Section CriarPage(Document document)
        {
            var section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.TopMargin = 80;
            section.PageSetup.BottomMargin = 80;

            return section;
        }

        private string TotalFaturamento(List<Faturamento> total)
        {
            return total.Select(z => z.valor).Sum().ToString();
            
        }
        private string PagamentoTexto(EnumRegistros tipo)
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

        private byte[] RenderDocumento(Document document)
        {
            var renderer = new PdfDocumentRenderer
            {
                Document = document
            };

            renderer.RenderDocument();
            using var file = new MemoryStream();
            renderer.PdfDocument.Save(file);

            return file.ToArray();
        }
    }
}
