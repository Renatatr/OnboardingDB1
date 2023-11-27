using Hospital.Dominio.Base;
using Hospital.Dominio.Dto;
using Hospital.Dominio.Entidades;
using Hospital.Dominio.Especialidades;
using Hospital.Dominio.Interfaces;
using Hospital.Dominio.Servicos;
using Moq;
using Xunit;

namespace Hospital.DominioTest.Servicos;

public class ArmazenadorDeMedicoTest
{
    private readonly ArmazenadorDeMedico _armazenadorDeMedico;
    private readonly Mock<IMedicoRepository> _medicoRepositoryMock;

    public ArmazenadorDeMedicoTest()
    {
        _medicoRepositoryMock = new Mock<IMedicoRepository>();
        _armazenadorDeMedico = new ArmazenadorDeMedico(_medicoRepositoryMock.Object);
    }

    [Fact]
    public void DeveCadastrarUmNovoMedico()
    {
        var medico = new MedicoDto();

        medico.Id = 0;
        medico.CRM = "pr123456";
        medico.CPF = "69179909060";
        medico.Especialidade = Especialidade.Neurologia;
        medico.Nome = "asdfgh";

        _medicoRepositoryMock
            .Setup(x => x.ConsultaUnica(It.IsAny<Func<Medico, bool>>()))
            .Returns((Medico)null);

        _armazenadorDeMedico.Armazenar(medico);

        _medicoRepositoryMock.Verify(x => x.Adicionar(It.Is<Medico>(y => y.Nome == medico.Nome &&
                                                                         y.Especialidade == medico.Especialidade &&
                                                                         y.CRM == medico.CRM &&
                                                                         y.CPF == medico.CPF)), Times.Once);
    }

    [Fact]
    public void NaoDeveCadastrarUmNovoMedicoRepetido()
    {
        var medico = new MedicoDto();

        medico.Id = 0;
        medico.CRM = "pr123456";
        medico.CPF = "69179909060";
        medico.Especialidade = Especialidade.Neurologia;
        medico.Nome = "asdfgh";

        _medicoRepositoryMock
            .Setup(y => y.ConsultaUnica(It.IsAny<Func<Medico, bool>>()))
            .Returns(new Medico());

        var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDeMedico.Armazenar(medico));

        Assert.Equal("Médico já cadastrado!", execessao.Message);
    }

    public static IEnumerable<object[]> casoDeTesteDtoInvalidoCadastrar =>
        new List<object[]>
        {
            new object[] { new MedicoDto(){
                Id = 0,
                Nome = "",
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "69179909060",
                CRM = "pr123456",
            }, Resource.NomeInvalido },
            new object[] { new MedicoDto(){
                Id = 0,
                Nome = null,
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "69179909060",
                CRM = "pr123456",
            }, Resource.NomeInvalido },
             new object[] { new MedicoDto(){
                Id = 0,
                Nome = "abc",
                Especialidade = (Especialidade)23,
                CPF = "69179909060",
                CRM = "pr123456",
             }, Resource.EspecialidadeInvalida },
             new object[] { new MedicoDto(){
                Id = 0,
                Nome = "abc",
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "1",
                CRM = "pr123456",
             }, Resource.CPFInvalido },
             new object[] { new MedicoDto(){
                Id = 0,
                Nome = "abc",
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "69179909060",
                CRM = "3",
             }, Resource.CRMInvalido },
};


    [Theory, MemberData(nameof(casoDeTesteDtoInvalidoCadastrar))]
    public void NaoDeveCadastrarUmMedicoQuandoInvalido(MedicoDto medicoDto, string resource)
    {
        _medicoRepositoryMock
           .Setup(x => x.ConsultaUnica(It.IsAny<Func<Medico, bool>>()))
           .Returns((Medico)null);

        var execessao = Assert.Throws(typeof(ExcecaoDeDominio), () => _armazenadorDeMedico.Armazenar(medicoDto));

        Assert.Equal(resource + ", ", execessao.Message);
    }

    [Fact]
    public void NaoDeveEditarUmMedicoQuandoEleForNulo()
    {
        var medico = new MedicoDto();

        medico.Id = 1;
        medico.CRM = "pr123456";
        medico.CPF = "69179909060";
        medico.Especialidade = Especialidade.Neurologia;
        medico.Nome = "asdfgh";

        _medicoRepositoryMock
            .Setup(y => y.ConsultaUnica(It.IsAny<Func<Medico, bool>>()))
            .Returns((Medico)null);

        var execessao = Assert.Throws(typeof(Exception), () => _armazenadorDeMedico.Armazenar(medico));

        Assert.Equal("Médico não encontrado", execessao.Message);
    }

    public static IEnumerable<object[]> casoDeTesteDtoInvalidoEditar =>
    new List<object[]>
    {
            new object[] { new MedicoDto(){
                Id = 1,
                Nome = "",
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "69179909060",
                CRM = "pr123456",
            }, Resource.NomeInvalido },
            new object[] { new MedicoDto(){
                Id = 1,
                Nome = null,
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "69179909060",
                CRM = "pr123456",
            }, Resource.NomeInvalido },
             new object[] { new MedicoDto(){
                Id = 1,
                Nome = "abc",
                Especialidade = (Especialidade)23,
                CPF = "69179909060",
                CRM = "pr123456",
             }, Resource.EspecialidadeInvalida },
             new object[] { new MedicoDto(){
                Id = 1,
                Nome = "abc",
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "1",
                CRM = "pr123456",
             }, Resource.CPFInvalido },
             new object[] { new MedicoDto(){
                Id = 1,
                Nome = "abc",
                Especialidade = Especialidade.ClinicoGeral,
                CPF = "69179909060",
                CRM = "3",
             }, Resource.CRMInvalido },
    };


    [Theory, MemberData(nameof(casoDeTesteDtoInvalidoEditar))]
    public void NaoDeveEditarUmMedicoQuandoInvalido(MedicoDto medicoDto, string resource)
    {
        _medicoRepositoryMock
           .Setup(x => x.ConsultaUnica(It.IsAny<Func<Medico, bool>>()))
           .Returns(new Medico());

        var execessao = Assert.Throws(typeof(ExcecaoDeDominio), () => _armazenadorDeMedico.Armazenar(medicoDto));

        Assert.Equal(resource + ", ", execessao.Message);
    }

    [Fact]
    public void DeveEditarUmNovoMedico()
    {
        var medico = new MedicoDto();

        medico.Id = 1;
        medico.CRM = "pr123456";
        medico.CPF = "69179909060";
        medico.Especialidade = Especialidade.Neurologia;
        medico.Nome = "asdfgh";

        _medicoRepositoryMock
            .Setup(x => x.ConsultaUnica(It.IsAny<Func<Medico, bool>>()))
            .Returns(new Medico());

        string mensagem = _armazenadorDeMedico.Armazenar(medico);

        Assert.Equal("atualizado!", mensagem);
    }
}