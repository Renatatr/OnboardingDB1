using Hospital.Dados.Contexto;
using Hospital.Dados.Repositorios.Base;
using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Dados.Repositorios;

public class ConsultaRepository : RepositoryBase<Consulta>, IConsultaRepository
{

    public ConsultaRepository(HospitalContext context) : base(context)
    {
    }

    public async Task<List<ConsultaCalendarioDto>> SelecionarPorIdRetornarNomesNaoId(int id)
    {

        var consultaDB = _context.Set<Consulta>();
        var medicoDB = _context.Set<Medico>();
        var pacienteDB = _context.Set<Paciente>();

        return await (
                        from consulta in consultaDB
                        join medico in medicoDB on consulta.MedicoId equals medico.Id
                        join paciente in pacienteDB on consulta.PacienteId equals paciente.Id
                        where consulta.Id == id
                        select new ConsultaCalendarioDto
                        {
                            NomeMedico = medico.Nome,
                            NomePaciente = paciente.Nome,
                            DataDaConsulta = consulta.Data,
                            DuracaoMin = consulta.DuracaoMin
                        }
                     ).ToListAsync();
    }
}
