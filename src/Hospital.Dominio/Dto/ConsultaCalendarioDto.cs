namespace Hospital.Dominio.Dto;

public class ConsultaCalendarioDto
{
    public string NomeMedico { get; set; }
    public string NomePaciente { get; set; }
    public DateTime DataDaConsulta { get; set; }
    public int DuracaoMin { get; set; }
}
