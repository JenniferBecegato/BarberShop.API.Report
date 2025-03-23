namespace Barber.Report.Interface
{
    internal interface IRelatório
    {
        public Task<byte[]> Relatorio(int? id);
    }
}
