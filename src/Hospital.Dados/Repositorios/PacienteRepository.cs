using Hospital.Dados.Contexto;
using Hospital.Dados.Repositorios.Base;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dados.Repositorios;

public class PacienteRepository : RepositoryBase<Paciente>, IPacienteRepository
{
    public PacienteRepository(HospitalContext context) : base(context)
    {
    }
}
