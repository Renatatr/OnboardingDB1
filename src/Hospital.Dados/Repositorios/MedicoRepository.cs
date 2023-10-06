using Hospital.Dados.Contexto;
using Hospital.Dados.Repositorios.Base;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dados.Repositorios;

public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
{
    public MedicoRepository(HospitalContext context) : base(context)
    {
    }
}
