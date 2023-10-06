using Hospital.Dominio.Entidades;
using Hospital.Dominio.Especialidades;

namespace Hospital.DominioTest.Builders;

internal class MedicoBuilder
{
    private string _nome = "josé";
    private Especialidade _especialidade = Especialidade.ClinicoGeral;
    private string _cpf = "33088167090";
    private string _crm = "pr123456";

    public static MedicoBuilder Novo()
    {
        return new MedicoBuilder();
    }

    public MedicoBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public Medico Build()
    {
        var medico = new Medico(_nome, _especialidade, _cpf, _crm);

        return medico;
    }
}