using ExpectedObjects;
using Hospital.Dominio.Base;
using Hospital.Dominio.Entidades;
using Hospital.DominioTest.Builders;
using Xunit;

namespace Hospital.DominioTest.Entidades;

public class ConsultaTest
{
    private readonly DateTime _data;

    public ConsultaTest()
    {
        _data = DateTime.Now;
    }

    [Fact]
    public void DeveCriarConsulta()
    {
        var consultaEsperada = new
        {
            MedicoId = MedicoBuilder.Novo().Build().Id,
            PacienteId = new Paciente("abc", DateTime.Now.Date, "asd", "63449115065").Id,
            Data = _data,
            DuracaoMin = 20
        };

        var consulta = new Consulta(consultaEsperada.MedicoId, consultaEsperada.PacienteId, consultaEsperada.Data, consultaEsperada.DuracaoMin);

        consultaEsperada.ToExpectedObject().ShouldMatch(consulta);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void NaoDeveCriaConsultaSemPaciente(int pacienteIdInvalido)
    {
        var consulta = new Consulta(2, pacienteIdInvalido, DateTime.Now.AddDays(1), 20);

        var mensagem = Assert.Throws<ExcecaoDeDominio>(() => consulta.Validar()).Message;
        Assert.Equal("Paciente Inválido!, ", mensagem);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void NaoDeveCriaConsultaSemMedico(int medicoIdInvalido)
    {
        var consulta = new Consulta(medicoIdInvalido, 10, DateTime.Now.AddDays(1), 20);

        var mensagem = Assert.Throws<ExcecaoDeDominio>(() => consulta.Validar()).Message;
        Assert.Equal("Médico Inválido!, ", mensagem);
    }
}
