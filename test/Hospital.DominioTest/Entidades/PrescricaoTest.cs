using ExpectedObjects;
using Hospital.Dominio.Base;
using Hospital.Dominio.Entidades;
using Xunit;

namespace Hospital.DominioTest.Entidades;

public class PrescricaoTest
{
    [Fact]
    public void DeveCriarPrescricao()
    {
        var prescricaoEsperada = new
        {
            ConsultaId = new Consulta(1,2,DateTime.Now, 20).Id,
            TratamentoId = 1,
            Diagnostico = "alguma coisa"
        };

        var prescricao = new Prescricao(prescricaoEsperada.ConsultaId, prescricaoEsperada.TratamentoId, prescricaoEsperada.Diagnostico);
        prescricaoEsperada.ToExpectedObject().ShouldMatch(prescricao);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("abc")]
    public void DeveTerDiagnosticoComMaisDeTresLetras(string str)
    {
        var prescricao = new Prescricao(1, 2, str);

        var mensagem = Assert.Throws<ExcecaoDeDominio>(() => prescricao.Validar()).Message;
        Assert.Equal(Resource.DiagnosticoInvalido + ", ", mensagem);
    }
}