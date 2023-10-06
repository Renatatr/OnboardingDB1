using Bogus;
using Bogus.Extensions.Brazil;
using ExpectedObjects;
using Hospital.Dominio.Base;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Especialidades;
using Hospital.DominioTest.Builders;
using Hospital.DominioTest.Util;
using System.Text.RegularExpressions;
using Xunit;

namespace Hospital.DominioTest.Entidades;

public class MedicoTest
{
    private readonly Faker _faker;
    private readonly string _nome;
    private readonly Especialidade _especialidade;
    private readonly string _cpf;
    private readonly string _crm;

    public MedicoTest()
    {
        _faker = new Faker();

        _nome = _faker.Random.Word();
        _especialidade = Especialidade.ClinicoGeral;
        _cpf = Regex.Replace(_faker.Person.Cpf(), @"[^a-zA-Z0-9\s]", "");
        _crm = "pr123456";
    }

    [Fact]
    public void DeveCriarMedico()
    {
        var medicoEsperado = new
        {
            Nome = _nome,
            Especialidade = _especialidade,
            CPF = _cpf,
            CRM = _crm
        };

        var medico = new Medico(medicoEsperado.Nome, medicoEsperado.Especialidade, medicoEsperado.CPF, medicoEsperado.CRM);

        medicoEsperado.ToExpectedObject().ShouldMatch(medico);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveTerMedicoComNomeInvalido(string nomeInvalido)
    {
        Assert.Throws<ExcecaoDeDominio>(() => MedicoBuilder.Novo().ComNome(nomeInvalido).Build().Validar()).ComMensagem(Resource.NomeInvalido + ", ");
    }

    [Fact]
    public void DeveAlterarNome()
    {
        var nomeEsperado = _faker.Person.FullName;
        var medico = MedicoBuilder.Novo().Build();

        medico.AlterarNome(nomeEsperado);

        Assert.Equal(nomeEsperado, medico.Nome);
    }
}