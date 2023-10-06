using Hospital.Dominio.Dto;

namespace Hospital.Dominio.Interfaces;

public interface IArmazenadorPaciente
{
    string Armazenar(PacienteDto dto);
}
