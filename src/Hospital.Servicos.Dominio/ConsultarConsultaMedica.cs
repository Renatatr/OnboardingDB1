using Hospital.Dominio.Dto;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dominio.Servicos;

public class ConsultarConsultaMedica : IConsultarConsultaMedica
{
    private readonly IConsultaRepository _consultaRepository;

    public ConsultarConsultaMedica(IConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    public async Task<List<ConsultaCalendarioDto>> Consultar(int id)
    {
        var consultaCalendario = await _consultaRepository.SelecionarPorIdRetornarNomesNaoId(id);

        return consultaCalendario;
    }
}
