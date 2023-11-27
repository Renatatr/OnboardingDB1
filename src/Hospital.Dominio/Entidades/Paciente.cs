using Hospital.Dominio.Base;
using System.Text.RegularExpressions;

namespace Hospital.Dominio.Entidades;

public class Paciente : IId
{
    public int Id { get; protected set; }
    public string Nome { get; private set; }
    public DateTime Nascimento { get; private set; }
    public int Idade => CalculaIdade(Nascimento.Date);
    public string CPF { get; private set; }
    public string Acompanhante { get; private set; }

    //  public virtual ICollection<Consulta> Consultas { get; set; }

    public Paciente()
    {
    }

    public Paciente(string nome, DateTime nascimento, string acompanhante, string cpf)
    {
        Nome = nome;
        Nascimento = nascimento.Date;
        Acompanhante = acompanhante;
        CPF = Regex.Replace(cpf, @"[^a-zA-Z0-9\s]", "");
    }

    public void AlterarNome(string nome)
    {
        Nome = nome;
    }

    public void AlterarDataNascimento(DateTime nascimento)
    {
        Nascimento = nascimento.Date;
    }

    public void AlterarCPF(string cpf)
    {
        CPF = Regex.Replace(cpf, @"[^a-zA-Z0-9\s]", "");
    }

    public void AlterarAcompanhante(string acompanhante)
    {
        Acompanhante = acompanhante;
    }

    public void Validar()
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(Nome), Resource.NomeInvalido)
            .Quando(Idade < 1, Resource.IdadeInvalida)
            .Quando(Idade < 18 && string.IsNullOrEmpty(Acompanhante), Resource.AcompanhanteObrigatorio)
            .Quando(!ValidaRegistrosOficiais.ValidateCPF(CPF), Resource.CPFInvalido)
            .DispararExcecaoSeExistir();
    }

    private int CalculaIdade(DateTime nascimento)
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - nascimento.Year;

        // Adjust age if birthday hasn't occurred yet this year
        if (hoje.Month < nascimento.Month || (hoje.Month == nascimento.Month && hoje.Day < nascimento.Day))
        {
            idade--;
        }

        return idade;
    }
}