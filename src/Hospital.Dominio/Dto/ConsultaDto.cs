namespace Hospital.Dominio.Dto;

public class ConsultaDto
{
    public int Id { get; set; }
    public int MedicoId { get; set; }
    public int PacienteId { get; set; }
    public DateTime Data { get; set; }
    public int DuracaoMin { get; set; }
}
