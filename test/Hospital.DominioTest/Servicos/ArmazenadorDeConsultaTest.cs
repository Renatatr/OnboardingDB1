using Hospital.Dominio.Base;
using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;
using Hospital.Dominio.Servicos;
using Moq;
using Xunit;

namespace Hospital.DominioTest.Servicos;

public class ArmazenadorDeConsultaTest
{
    private readonly ArmazenadorDeConsulta _armazenadorDeConsulta;
    private readonly Mock<IConsultaRepository> _consultaRepositoryMock;

    public ArmazenadorDeConsultaTest()
    {
        _consultaRepositoryMock = new Mock<IConsultaRepository>();
        _armazenadorDeConsulta = new ArmazenadorDeConsulta(_consultaRepositoryMock.Object);
    }

    [Fact]
    public void DeveCadastrarUmaNovaConsulta()
    {
        var consulta = new ConsultaDto();

        consulta.Id = 0;
        consulta.MedicoId = 1;
        consulta.PacienteId = 1;
        consulta.Data = DateTime.Now;
        consulta.DuracaoMin = 20;

        _consultaRepositoryMock
            .Setup(x => x.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
            .Returns((Consulta)null);

        _armazenadorDeConsulta.Armazenar(consulta);

        _consultaRepositoryMock.Verify(x => x.Adicionar(It.Is<Consulta>(y => y.MedicoId == consulta.MedicoId && y.PacienteId == consulta.PacienteId)), Times.Once);
    }

    [Fact]
    public void NaoDeveCadastrarUmaNovaConsultaRepetida()
    {
        var consulta = new ConsultaDto();

        consulta.Id = 0;
        consulta.MedicoId = 1;
        consulta.PacienteId = 1;
        consulta.Data = DateTime.Now;
        consulta.DuracaoMin = 20;

        _consultaRepositoryMock
            .Setup(y => y.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
            .Returns(new Consulta());

        var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDeConsulta.Armazenar(consulta));

        Assert.Equal("Consulta já cadastrada!", execessao.Message);
    } 
    
    [Fact]
    public void NaoDeveCadastrarUmaNovaConsultaSeMedicoOcupado()
    {
        var consulta = new ConsultaDto();

        consulta.Id = 0;
        consulta.MedicoId = 1;
        consulta.PacienteId = 1;
        consulta.Data = DateTime.Now;
        consulta.DuracaoMin = 20;

        _consultaRepositoryMock
            .SetupSequence(x => x.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
            .Returns((Consulta)null)
            .Returns(new Consulta());

        var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDeConsulta.Armazenar(consulta));

        Assert.Equal("Médico ocupado!", execessao.Message);
    }

    [Fact]
    public void NaoDeveCadastrarUmaNovaConsultaSePacienteOcupado()
    {
        var consulta = new ConsultaDto();

        consulta.Id = 0;
        consulta.MedicoId = 1;
        consulta.PacienteId = 1;
        consulta.Data = DateTime.Now;
        consulta.DuracaoMin = 20;

        _consultaRepositoryMock
            .SetupSequence(x => x.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
            .Returns((Consulta)null)
            .Returns((Consulta)null)
            .Returns(new Consulta());

        var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDeConsulta.Armazenar(consulta));

        Assert.Equal("Paciente ocupado!", execessao.Message);
    }

    public static IEnumerable<object[]> casoDeTesteDtoInvalidoCadastrar =>
    new List<object[]>
    {
            new object[] { new ConsultaDto(){
                 Id = 0,
                 Data = DateTime.Today.AddDays(-1),
                 MedicoId = 1,
                 PacienteId = 1,
                 DuracaoMin = 20,
            }, Resource.DataInvalida },
             new object[] { new ConsultaDto(){
                 Id = 0,
                 Data = DateTime.Today,
                 MedicoId = 1,
                 PacienteId = 1,
                 DuracaoMin = -8,
             }, Resource.DuracaoInvalida },
             new object[] { new ConsultaDto(){
                 Id = 0,
                 Data = DateTime.Today,
                 MedicoId = -81,
                 PacienteId = 1,
                 DuracaoMin = 8,
             }, Resource.MedicoInvalido },
             new object[] { new ConsultaDto(){
                 Id = 0,
                 Data = DateTime.Today,
                 MedicoId = 1,
                 PacienteId = -91,
                 DuracaoMin = 8,
             }, Resource.PacienteInvalido },
    };

    [Theory, MemberData(nameof(casoDeTesteDtoInvalidoCadastrar))]
    public void NaoDeveCadastrarUmaConsultaQuandoInvalida(ConsultaDto consultaDto, string resource)
    {
        _consultaRepositoryMock
           .Setup(x => x.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
           .Returns((Consulta)null);

        var execessao = Assert.Throws(typeof(ExcecaoDeDominio), () => _armazenadorDeConsulta.Armazenar(consultaDto));

        Assert.Equal(resource + ", ", execessao.Message);
    }

    [Fact]
    public void NaoDeveEditarUmaConsultaQuandoElaForNula()
    {
       var consulta = new ConsultaDto();

       consulta.Id = 2;

       _consultaRepositoryMock
           .Setup(x => x.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
           .Returns((Consulta)null);

       var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDeConsulta.Armazenar(consulta));

       Assert.Equal("Consulta não encontrada", execessao.Message);
    }

    public static IEnumerable<object[]> casoDeTesteDtoInvalidoEditar => 
        new List<object[]>
        {
            new object[] { new ConsultaDto(){
                Id = 1,
                Data = DateTime.Today.AddDays(-1),
                MedicoId = 1,
                PacienteId = 1,
                DuracaoMin = 20,
            }, Resource.DataInvalida },
             new object[] { new ConsultaDto(){
                 Id = 1,
                 Data = DateTime.Today,
                 MedicoId = 1,
                 PacienteId = 1,
                 DuracaoMin = -8,
             }, Resource.DuracaoInvalida },
             new object[] { new ConsultaDto(){
                 Id = 1,
                 Data = DateTime.Today,
                 MedicoId = -81,
                 PacienteId = 1,
                 DuracaoMin = 8,
             }, Resource.MedicoInvalido },
             new object[] { new ConsultaDto(){
                 Id = 1,
                 Data = DateTime.Today,
                 MedicoId = 1,
                 PacienteId = -91,
                 DuracaoMin = 8,
             }, Resource.PacienteInvalido },
        }; 


    [Theory, MemberData(nameof(casoDeTesteDtoInvalidoEditar))]
    public void NaoDeveEditarUmaConsultaQuandoInvalida(ConsultaDto consultaDto, string resource)
    {
        _consultaRepositoryMock
           .Setup(x => x.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
           .Returns(new Consulta());

       var execessao = Assert.Throws(typeof(ExcecaoDeDominio), () => _armazenadorDeConsulta.Armazenar(consultaDto));

       Assert.Equal(resource+", ", execessao.Message);
    }

    [Fact]
    public void DeveEditarUmaNovaConsulta()
    {
        var consulta = new ConsultaDto();

        consulta.Id = 1;
        consulta.MedicoId = 1;
        consulta.PacienteId = 1;
        consulta.Data = DateTime.Now;
        consulta.DuracaoMin = 20;

        _consultaRepositoryMock
            .Setup(x => x.ConsultaUnica(It.IsAny<Func<Consulta, bool>>()))
            .Returns(new Consulta());

        string mensagem = _armazenadorDeConsulta.Armazenar(consulta);

        Assert.Equal("atualizada!", mensagem);
    }
}