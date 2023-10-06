using Hospital.Dados.Contexto;
using Hospital.Dados.Repositorios.Base;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dados.Repositorios;

public class ConsultaRepository : RepositoryBase<Consulta>, IConsultaRepository
{
    public ConsultaRepository(HospitalContext context) : base(context)
    {
    }
}
