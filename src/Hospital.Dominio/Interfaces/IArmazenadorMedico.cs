using Hospital.Dominio.Dto;

namespace Hospital.Dominio.Interfaces;

public interface IArmazenadorMedico
{
    string Armazenar(MedicoDto dto);
}
