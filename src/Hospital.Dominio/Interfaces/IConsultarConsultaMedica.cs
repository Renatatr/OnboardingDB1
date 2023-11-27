using Hospital.Dominio.Dto;

namespace Hospital.Dominio.Interfaces;

public interface IConsultarConsultaMedica
{
    Task<List<ConsultaCalendarioDto>> Consultar(int id);
}
