using Hospital.Dominio.Base;

namespace Hospital.Dominio.Entidades;

public class Consulta : IId
{
    public int Id { get; private set; }
    public int MedicoId { get; private set; }
    public virtual Medico Medico { get; private set; }
    public int PacienteId { get; private set; }
    public virtual Paciente Paciente { get; private set; }
    public DateTime Data { get; private set; }
    public int DuracaoMin { get; private set; }

    public Consulta()
    {
    }

    public Consulta(int medicoId, int pacienteId, DateTime data, int duracaoMin)
    {
        MedicoId = medicoId;
        PacienteId = pacienteId;
        Data = data;
        DuracaoMin = duracaoMin;
    }

    public void AlterarMedico(int medicoId)
    {
        MedicoId = medicoId;
    }

    public void AlterarPaciente(int pacienteId)
    {
        PacienteId = pacienteId;
    }

    public void AlterarData(DateTime data)
    {
        Data = data;
    }

    public void AlterarDuracao(int duracaoMin)
    {
        DuracaoMin = duracaoMin;
    }
    
    public void Validar()
    {
        ValidadorDeRegra.Novo()
            .Quando(Data < DateTime.Today, Resource.DataInvalida)
            .Quando(DuracaoMin < 1, Resource.DuracaoInvalida)
            .Quando(MedicoId <= 0, Resource.MedicoInvalido)
            .Quando(PacienteId <= 0, Resource.PacienteInvalido)
            .DispararExcecaoSeExistir();
    }
}