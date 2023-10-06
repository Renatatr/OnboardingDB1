namespace Hospital.Dominio.Dto;

public class PacienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime Nascimento { get; set; }
    public string CPF { get; set; }
    public string Acompanhante { get; set; }
}
