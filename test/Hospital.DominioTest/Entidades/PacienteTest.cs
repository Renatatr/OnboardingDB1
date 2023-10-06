using Bogus;
using Bogus.Extensions.Brazil;
using ExpectedObjects;
using Hospital.Dominio.Entidades;
using System.Text.RegularExpressions;
using Xunit;

namespace Hospital.DominioTest.Entidades;

public class PacienteTest
{
    private readonly Faker _faker;
    private readonly string _nome;
    private readonly DateTime _nascimento;
    private readonly string _nomeAcompanhante;
    private readonly string _cpf;

    public PacienteTest()
    {
        _faker = new Faker();

        _nome = _faker.Person.FullName;
        _nascimento = _faker.Person.DateOfBirth;
        _nomeAcompanhante = _faker.Person.FullName;
        _cpf = Regex.Replace(_faker.Person.Cpf(), @"[^a-zA-Z0-9\s]", "");
    }

    [Fact]
    public void DeveCriarPaciente()
    {
        var pacienteEsperado = new
        {
            Nome = _nome,
            Nascimento = _nascimento.Date,
            Acompanhante = _nomeAcompanhante,
            CPF = _cpf
        };

        var paciente = new Paciente(pacienteEsperado.Nome, pacienteEsperado.Nascimento, pacienteEsperado.Acompanhante, pacienteEsperado.CPF);

        pacienteEsperado.ToExpectedObject().ShouldMatch(paciente);
    }
}