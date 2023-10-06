using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dominio.Servicos;

public class ArmazenadorDePrescricao : IArmazenadorPrescricao
{
    private readonly IPrescricaoRepository _prescricaoRepository;

    public ArmazenadorDePrescricao(IPrescricaoRepository prescricaoRepository)
    {
        _prescricaoRepository = prescricaoRepository;
    }

    public string Armazenar(PrescricaoDto dto)
    {
        if (dto.Id == 0)
        {
            Cadastrar(dto);
            return "cadastrada!";
        }
        else
        {
            Editar(dto);
            return "atualizada!";
        }
    }

    private void Cadastrar(PrescricaoDto dto)
    {
        var validadorPrescricaoRepetida = _prescricaoRepository.ConsultaUnica(x => x.ConsultaId == dto.ConsultaId &&
                                                                                   x.Diagnostico == dto.Diagnostico &&
                                                                                   x.TratamentoId == dto.TratamentoId);
        if (validadorPrescricaoRepetida != null)
        {
            throw new Exception("Prescrição já cadastrada!");
        }

        var entidade = new Prescricao(dto.ConsultaId, dto.TratamentoId, dto.Diagnostico);
        entidade.Validar();
        _prescricaoRepository.Adicionar(entidade);

    }

    private void Editar(PrescricaoDto dto)
    {
        var prescricao = _prescricaoRepository.ConsultaUnica(x => x.Id == dto.Id);
        if (prescricao == null)
        {
            throw new Exception("Prescrição não encontrada");
        }

        prescricao.AlterarDiagnostico(dto.Diagnostico);
        prescricao.AlterarTratamento(dto.TratamentoId);
        prescricao.AlterarConsulta(dto.ConsultaId);
        prescricao.Validar();
        _prescricaoRepository.Alterar(prescricao);
    }
}
