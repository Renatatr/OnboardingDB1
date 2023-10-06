using Hospital.Dominio.Dto;

namespace Hospital.Dominio.Interfaces;

public interface IArmazenadorTratamento
{
    string Armazenar(TratamentoDto dto);
}
