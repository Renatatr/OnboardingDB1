using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dominio.Servicos;

public class ArmazenadorDeTratamento : IArmazenadorTratamento
{
    private readonly ITratamentoRepository _tratamentoRepository;

    public ArmazenadorDeTratamento(ITratamentoRepository tratamentoRepository)
    {
        _tratamentoRepository = tratamentoRepository;
    }

    public string Armazenar(TratamentoDto dto)
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
    
    private void Cadastrar(TratamentoDto dto)
    {
        var validadorConsultaRepetida = _tratamentoRepository.ConsultaUnica(x => x.Nome == dto.Nome && x.Periodo == dto.Periodo && x.ModoDeUso == dto.ModoDeUso);
        if (validadorConsultaRepetida != null)
        {
            throw new Exception("Tratamento já cadastrado!");
        }
        
        var entidade = new Tratamento(dto.Nome,dto.Periodo,dto.ModoDeUso);
        entidade.Validar();
        _tratamentoRepository.Adicionar(entidade);
    }

    private void Editar(TratamentoDto dto)
    {
        var tratamento = _tratamentoRepository.ConsultaUnica(x => x.Id == dto.Id);
        if (tratamento == null)
        {
            throw new Exception("Consulta não encontrada");
        }

        tratamento.AlterarNome(dto.Nome);
        tratamento.AlterarPeriodo(dto.Periodo);
        tratamento.AlterarModoDeUso(dto.ModoDeUso);
        tratamento.Validar();
        _tratamentoRepository.Alterar(tratamento);
    }
}
