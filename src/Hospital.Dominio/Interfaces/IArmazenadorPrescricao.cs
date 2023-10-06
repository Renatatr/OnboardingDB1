using Hospital.Dominio.Dto;

namespace Hospital.Dominio.Interfaces;

public interface IArmazenadorPrescricao
{
    string Armazenar(PrescricaoDto dto);
}
