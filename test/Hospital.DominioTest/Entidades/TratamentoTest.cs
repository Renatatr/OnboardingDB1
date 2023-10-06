using Bogus;
using ExpectedObjects;
using Hospital.Dominio.Entidades;
using Xunit;

namespace Hospital.DominioTest.Entidades;

public class TratamentoTest
{
    private readonly Faker _faker;

    public TratamentoTest()
    {
        _faker = new Faker();
    }

    [Fact]
    public void DeveCriarTratamento()
    {
        var tratamentoEsperado = new
        {
            Nome = _faker.Random.Word(),
            Periodo = _faker.Random.Number(),
            ModoDeUso = _faker.Random.Words()
        };

        var tratamento = new Tratamento(tratamentoEsperado.Nome, tratamentoEsperado.Periodo, tratamentoEsperado.ModoDeUso);

        tratamentoEsperado.ToExpectedObject().ShouldMatch(tratamento);
    }
}