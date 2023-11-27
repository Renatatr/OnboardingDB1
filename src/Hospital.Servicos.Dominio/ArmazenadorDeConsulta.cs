using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dominio.Servicos;

public class ArmazenadorDeConsulta : IArmazenadorConsulta
{
    private readonly IConsultaRepository _consultaRepository;

    public ArmazenadorDeConsulta(IConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    public string Armazenar(ConsultaDto dto)
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
    
    private void Cadastrar(ConsultaDto dto)
    {
        var validadorConsultaRepetida = _consultaRepository.ConsultaUnica(x => x.MedicoId == dto.MedicoId &&
                                                                               x.PacienteId == dto.PacienteId && 
                                                                               x.Data == dto.Data);
        if (validadorConsultaRepetida != null)
        {
            throw new Exception("Consulta já cadastrada!");
        }

        var validadorMedico = _consultaRepository.ConsultaUnica(x => x.MedicoId == dto.MedicoId &&
                                                                     x.Data.AddMinutes(x.DuracaoMin) >= dto.Data &&
                                                                     x.Data <= dto.Data);
        if (validadorMedico != null)
        {
            throw new Exception("Médico ocupado!");
        }

        var validadorPaciente = _consultaRepository.ConsultaUnica(x => x.PacienteId == dto.PacienteId &&
                                                                       x.Data.AddMinutes(x.DuracaoMin) >= dto.Data &&
                                                                       x.Data <= dto.Data);
        if (validadorPaciente != null)
        {
            throw new Exception("Paciente ocupado!");
        }
        
        var entidade = new Consulta(dto.MedicoId, dto.PacienteId, dto.Data, dto.DuracaoMin);
        entidade.Validar();
        _consultaRepository.Adicionar(entidade);

    }

    private void Editar(ConsultaDto dto)
    {
        var consulta = _consultaRepository.ConsultaUnica(x => x.Id == dto.Id);
        if (consulta == null)
        {
            throw new Exception("Consulta não encontrada");
        }
        
        consulta.AlterarMedico(dto.MedicoId);
        consulta.AlterarPaciente(dto.PacienteId);
        consulta.AlterarData(dto.Data);
        consulta.AlterarDuracao(dto.DuracaoMin);
        consulta.Validar();
        _consultaRepository.Alterar(consulta);
    }
}
