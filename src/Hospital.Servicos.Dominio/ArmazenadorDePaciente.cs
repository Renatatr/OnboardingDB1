using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;
using System.Text.RegularExpressions;

namespace Hospital.Dominio.Servicos;

public class ArmazenadorDePaciente : IArmazenadorPaciente
{
    private readonly IPacienteRepository _pacienteRepository;

    public ArmazenadorDePaciente(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public string Armazenar(PacienteDto dto)
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

    private void Cadastrar(PacienteDto dto)
    {
        var validadorPacienteRepetido = _pacienteRepository.ConsultaUnica(x => x.CPF == Regex.Replace(dto.CPF, @"[^a-zA-Z0-9\s]", ""));
        if (validadorPacienteRepetido != null)
        {
            throw new Exception("Paciente já cadastrado!");
        }
        var entidade = new Paciente(dto.Nome, dto.Nascimento, dto.Acompanhante, dto.CPF);
        entidade.Validar();
        _pacienteRepository.Adicionar(entidade);
    }

    private void Editar(PacienteDto dto)
    {
        var paciente = _pacienteRepository.ConsultaUnica(x => x.Id == dto.Id);
        if (paciente == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        paciente.AlterarNome(dto.Nome);
        paciente.AlterarDataNascimento(dto.Nascimento);
        paciente.AlterarCPF(dto.CPF);
        paciente.AlterarAcompanhante(dto.Acompanhante);
        paciente.Validar();
        _pacienteRepository.Alterar(paciente);
    }
}
