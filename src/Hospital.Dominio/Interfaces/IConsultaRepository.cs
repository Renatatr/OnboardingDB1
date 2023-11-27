using Hospital.Dominio.Base;
using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;

namespace Hospital.Dominio.Interfaces;

public interface IConsultaRepository : IRepositoryBase<Consulta>
{
    public Task<List<ConsultaCalendarioDto>> SelecionarPorIdRetornarNomesNaoId(int id);
}