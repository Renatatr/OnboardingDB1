using Hospital.Dominio.Especialidades;

namespace Hospital.Dominio.Dto;

public class MedicoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public Especialidade Especialidade { get; set; }
    public string CPF { get; set; }
    public string CRM { get; set; }
}
