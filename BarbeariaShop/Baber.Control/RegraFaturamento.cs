using AutoMapper;
using Baber.Control.BarberDB;
using Baber.Control.Filtro;
using Baber.Model.Entity;
using Baber.Model.Request.Registros;
using Baber.Model.Response;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Baber.Control
{
    public class RegraFaturamento
    {
        private readonly IMapper _mapper;
        public RegraFaturamento(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ResponseRegistro Salvar(Registros request)
        {
            try
            {
                var banco = new BarberShopDB();

                Faturamento result = _mapper.Map<Faturamento>(request);
                Validar(request);

                banco.faturamento.Add(result);
                banco.SaveChanges();

                var Resposta = new ResponseRegistro()
                {
                    Titulo = result.Titulo,
                    Descricao = result.descricao,
                    Pagamento = result.pagamento,
                    Valor = result.valor,
                };
                return Resposta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void Atualizar(Registros request, int id)
        {
            try
            {
                var banco = new BarberShopDB();
                Validar(request);

                Faturamento faturamento_salvo_atualmente = ListaRegistro(id).FirstOrDefault();
                _mapper.Map(request, faturamento_salvo_atualmente);
                
                banco.Update(faturamento_salvo_atualmente);
                banco.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Deletar(int id)
        {
            bool consegui_deletar = false;
            try
            {
                var banco = new BarberShopDB();

                Faturamento faturamento_salvo_atualmente = ListaRegistro(id).FirstOrDefault();
                if (faturamento_salvo_atualmente != null)
                {
                    banco.Remove(faturamento_salvo_atualmente);
                    banco.SaveChanges();
                    consegui_deletar = true;
                }
                else
                {
                    consegui_deletar = false;
                }

                return consegui_deletar;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        private void Validar(Registros request)
        {
            var validator = new ValidatorRegistros();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var Listaerros = result.Errors.Select(x => x.ErrorMessage).ToList();

                throw new Exception(string.Join(",", Listaerros));
            }
        }

        public List<Faturamento> ListaRegistro(int?id = null)
        {
            var banco = new BarberShopDB();
            
            if (id != null)
            {
                return banco.faturamento.Where(x => x.Id == id).ToList();
            }
            else
            {
                return banco.faturamento.ToList();
            }

        }


    }
}
