using Hospital.Dominio.Dto;

namespace Hospital.Dominio.Interfaces;

public interface IArmazenadorConsulta
{
    string Armazenar(ConsultaDto dto);
}
