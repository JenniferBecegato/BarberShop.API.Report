using Barber.Realatório.Pdf;
using Barber.Report.Relatório;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;


namespace Barber.API.Controllers
{
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        [HttpGet("RelatórioFaturamentoPdf")]
        public async Task<IActionResult> GetAllPdf()
        {
            Pdf ImportPdf = new Pdf();
            byte[] file = await ImportPdf.Relatorio(null);

            return File(file, MediaTypeNames.Application.Pdf, "Faturamento.pdf");
        }

        [HttpGet("RelatórioFaturamento_porID_Pdf/{id}")]
        public async Task<IActionResult> GetIDPdf(int id)
        {
            Pdf ImportPdf = new Pdf();
            byte[] file = await ImportPdf.Relatorio(id);

            return File(file, MediaTypeNames.Application.Pdf, "Faturamento.pdf");
        }

        [HttpGet("RelatórioFaturamentoExcel")]

        public async Task<IActionResult> GetAllExcel()
        {
            Excel ExcelAll = new Excel();
            byte[] file = await ExcelAll.Relatorio(null);

            return File(file, MediaTypeNames.Application.Octet, "Faturamento_excel.xlsx");
        }
    }
}
