using Hospital.Dominio.Base;
using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Interfaces;
using Hospital.Dominio.Servicos;
using Moq;
using Xunit;

namespace Hospital.DominioTest.Servicos;

public class ArmazenadorDePacienteTest
{
    private readonly ArmazenadorDePaciente _armazenadorDePaciente;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;

    public ArmazenadorDePacienteTest()
    {
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        _armazenadorDePaciente = new ArmazenadorDePaciente(_pacienteRepositoryMock.Object);
    }

    [Fact]
    public void DeveCadastrarUmNovoPaciente()
    {
        var paciente = new PacienteDto();

        paciente.Id = 0;
        paciente.Nome = "asdf";
        paciente.Nascimento = new DateTime(1985,04,06);
        paciente.Acompanhante = "cdsa";
        paciente.CPF = "69179909060";

        _pacienteRepositoryMock
            .Setup(x => x.ConsultaUnica(It.IsAny<Func<Paciente, bool>>()))
            .Returns((Paciente)null);

        _armazenadorDePaciente.Armazenar(paciente);

        _pacienteRepositoryMock.Verify(x => x.Adicionar(It.Is<Paciente>(y => y.Nome == paciente.Nome &&
                                                                             y.Nascimento == paciente.Nascimento &&
                                                                             y.Acompanhante == paciente.Acompanhante &&
                                                                             y.CPF == paciente.CPF)), Times.Once);
    }

    [Fact]
    public void NaoDeveCadastrarUmNovoPacienteRepetido()
    {
        var paciente = new PacienteDto();

        paciente.Id = 0;
        paciente.Nome = "asdf";
        paciente.Nascimento = new DateTime(1985, 04, 06);
        paciente.Acompanhante = "cdsa";
        paciente.CPF = "69179909060";

        _pacienteRepositoryMock
            .Setup(y => y.ConsultaUnica(It.IsAny<Func<Paciente, bool>>()))
            .Returns(new Paciente());

        var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDePaciente.Armazenar(paciente));

        Assert.Equal("Paciente já cadastrado!", execessao.Message);
    }

    public static IEnumerable<object[]> casoDeTesteDtoInvalidoCadastrar =>
        new List<object[]>
        {
            new object[] { new PacienteDto(){
                Id = 0,
                Nome = "",
                Nascimento = new DateTime(1985, 04, 06),
                Acompanhante = "cdsa",
                CPF = "69179909060",
            }, Resource.NomeInvalido },
            new object[] { new PacienteDto(){
                Id = 0,
                Nome = null,
                Nascimento = new DateTime(1985, 04, 06),
                Acompanhante = "cdsa",
                CPF = "69179909060",
            }, Resource.NomeInvalido },
             new object[] { new PacienteDto(){
                Id = 0,
                Nome = "asdf",
                Nascimento = DateTime.Now.AddDays(1),
                Acompanhante = "cdsa",
                CPF = "69179909060",
             }, Resource.IdadeInvalida },
             new object[] { new PacienteDto(){
                Id = 0,
                Nome = "asdf",
                Nascimento = DateTime.Now.AddYears(-2),
                Acompanhante = "",
                CPF = "69179909060",
             }, Resource.AcompanhanteObrigatorio },
             new object[] { new PacienteDto(){
                Id = 0,
                Nome = "asdf",
                Nascimento = new DateTime(1985, 04, 06),
                Acompanhante = "cdsa",
                CPF = "7",
             }, Resource.CPFInvalido },
    };


    [Theory, MemberData(nameof(casoDeTesteDtoInvalidoCadastrar))]
    public void NaoDeveCadastrarUmPacienteQuandoInvalido(PacienteDto medicoDto, string resource)
    {
        _pacienteRepositoryMock
           .Setup(x => x.ConsultaUnica(It.IsAny<Func<Paciente, bool>>()))
           .Returns((Paciente)null);

        var execessao = Assert.Throws(typeof(ExcecaoDeDominio), () => _armazenadorDePaciente.Armazenar(medicoDto));

        Assert.Equal(resource + ", ", execessao.Message);
    }

    [Fact]
    public void NaoDeveEditarUmPacienteQuandoEleForNulo()
    {
        var paciente = new PacienteDto();

        paciente.Id = 2;
        paciente.Nome = "asdf";
        paciente.Nascimento = new DateTime(1985, 04, 06);
        paciente.Acompanhante = "cdsa";
        paciente.CPF = "69179909060";

        _pacienteRepositoryMock
            .Setup(y => y.ConsultaUnica(It.IsAny<Func<Paciente, bool>>()))
            .Returns((Paciente)null);

        var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDePaciente.Armazenar(paciente));

        Assert.Equal("Paciente não encontrado", execessao.Message);
    }

    public static IEnumerable<object[]> casoDeTesteDtoInvalidoEditar =>
     new List<object[]>
     {
            new object[] { new PacienteDto(){
                Id = 1,
                Nome = "",
                Nascimento = new DateTime(1985, 04, 06),
                Acompanhante = "cdsa",
                CPF = "69179909060",
            }, Resource.NomeInvalido },
            new object[] { new PacienteDto(){
                Id = 1,
                Nome = null,
                Nascimento = new DateTime(1985, 04, 06),
                Acompanhante = "cdsa",
                CPF = "69179909060",
            }, Resource.NomeInvalido },
             new object[] { new PacienteDto(){
                Id = 1,
                Nome = "asdf",
                Nascimento = new DateTime(2028, 04, 06),
                Acompanhante = "asdf",
                CPF = "69179909060",
             }, Resource.IdadeInvalida },
             new object[] { new PacienteDto(){
                Id = 1,
                Nome = "asdf",
                Nascimento = DateTime.Now.AddYears(-2),
                Acompanhante = "",
                CPF = "69179909060",
             }, Resource.AcompanhanteObrigatorio },
             new object[] { new PacienteDto(){
                Id = 1,
                Nome = "asdf",
                Nascimento = new DateTime(1985, 04, 06),
                Acompanhante = "cdsa",
                CPF = "7",
             }, Resource.CPFInvalido },
     };


    [Theory, MemberData(nameof(casoDeTesteDtoInvalidoEditar))]
    public void NaoDeveEditarUmPacienteQuandoInvalido(PacienteDto pacienteDto, string resource)
    {
        _pacienteRepositoryMock
           .Setup(x => x.ConsultaUnica(It.IsAny<Func<Paciente, bool>>()))
           .Returns(new Paciente());

        var execessao = Assert.Throws(typeof(ExcecaoDeDominio), () => _armazenadorDePaciente.Armazenar(pacienteDto));

        Assert.Equal(resource + ", ", execessao.Message);
    }

    [Fact]
    public void DeveEditarUmNovoPaciente()
    {
        var paciente = new PacienteDto();

        paciente.Id = 2;
        paciente.Nome = "asdf";
        paciente.Nascimento = new DateTime(1985, 04, 06);
        paciente.Acompanhante = "cdsa";
        paciente.CPF = "69179909060";

        _pacienteRepositoryMock
            .Setup(x => x.ConsultaUnica(It.IsAny<Func<Paciente, bool>>()))
            .Returns(new Paciente());

        string mensagem = _armazenadorDePaciente.Armazenar(paciente);

        Assert.Equal("atualizado!", mensagem);
    }
}