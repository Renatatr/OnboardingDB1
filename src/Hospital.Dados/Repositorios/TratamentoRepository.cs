using Hospital.Dados.Contexto;
using Hospital.Dados.Repositorios.Base;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dados.Repositorios;

public class TratamentoRepository : RepositoryBase<Tratamento>, ITratamentoRepository
{
    public TratamentoRepository(HospitalContext context) : base(context)
    {
    }
}
