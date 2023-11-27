using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;
using System.Text.RegularExpressions;

namespace Hospital.Dominio.Servicos;

public class ArmazenadorDeMedico : IArmazenadorMedico
{
    private readonly IMedicoRepository _medicoRepository;

    public ArmazenadorDeMedico(IMedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    public string Armazenar(MedicoDto dto)
    {
        if (dto.Id == 0)
        {
            Cadastrar(dto);
            return "cadastrado!";
        }
        else
        {
            Editar(dto);
            return "atualizado!";
        }
    }
    
    private void Cadastrar(MedicoDto dto)
    {
        var validadorMedicoRepetido = _medicoRepository.ConsultaUnica(x => x.CPF == Regex.Replace(dto.CPF, @"[^a-zA-Z0-9\s]", ""));
        if (validadorMedicoRepetido != null)
        {
            throw new Exception("Médico já cadastrado!");
        }
        var entidade = new Medico(dto.Nome, dto.Especialidade, dto.CPF, dto.CRM);
        entidade.Validar();
        _medicoRepository.Adicionar(entidade);
    }

    private void Editar(MedicoDto dto)
    {
        var medico = _medicoRepository.ConsultaUnica(x => x.Id == dto.Id);
        if (medico == null)
        {
            throw new Exception("Médico não encontrado");
        }
        
        medico.AlterarNome(dto.Nome);
        medico.AlterarEspecialidade(dto.Especialidade);
        medico.AlterarCPF(dto.CPF);
        medico.AlterarCRM(dto.CRM);
        medico.Validar();
        _medicoRepository.Alterar(medico);
    }
}
