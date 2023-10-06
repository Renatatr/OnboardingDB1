using Hospital.Dominio.Base;
using Hospital.Dominio.Interfaces;

namespace Hospital.Dominio.Entidades;

public class Prescricao : IId
{
    public int Id { get; set; }
    public int ConsultaId { get; set; }
    public virtual Consulta Consultas { get; set; }
    public int TratamentoId { get; set; }
    public virtual Tratamento Tratamentos { get; set; }
    public string Diagnostico { get; set; }

    private readonly IPrescricaoRepository _repositoryPrescricao;

    public Prescricao(int consultaId, int tratamentoId, string diagnostico)
    {
        ConsultaId = consultaId;
        TratamentoId = tratamentoId;
        Diagnostico = diagnostico;
    }


    public void AlterarDiagnostico(string diagnostico)
    {
        Diagnostico = diagnostico;
    }

    public void AlterarTratamento(int tratamentoId)
    {
        TratamentoId = tratamentoId;
    }

    public void AlterarConsulta(int consultaId)
    {
        ConsultaId = consultaId;
    }

    public void Validar()
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(Diagnostico) || Diagnostico.Length < 4, Resource.DiagnosticoInvalido)
            .DispararExcecaoSeExistir();
    }
}
