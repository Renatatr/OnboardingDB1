using Hospital.Dados.Contexto;
using Hospital.Dados.Repositorios.Base;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dados.Repositorios;

public class PrescricaoRepository : RepositoryBase<Prescricao>, IPrescricaoRepository
{
    public PrescricaoRepository(HospitalContext context) : base(context)
    {
    }
}
