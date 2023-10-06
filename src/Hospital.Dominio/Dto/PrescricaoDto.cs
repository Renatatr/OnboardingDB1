namespace Hospital.Dominio.Dto;

public class PrescricaoDto
{
    public int Id { get; set; }
    public int ConsultaId { get; set; }
    public int TratamentoId { get; set; }
    public string Diagnostico { get; set; }
}
