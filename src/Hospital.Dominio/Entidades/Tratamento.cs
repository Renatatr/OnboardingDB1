using Hospital.Dominio.Base;

namespace Hospital.Dominio.Entidades;

public class Tratamento : IId
{
    public int Id { get; protected set; }
    public string Nome { get; private set; }
    public int Periodo { get; private set; }
    public string ModoDeUso { get; private set; }

    public Tratamento(string nome, int periodo, string modoDeUso)
    {
        Nome = nome;
        Periodo = periodo;
        ModoDeUso = modoDeUso;
    }

    public void AlterarNome(string nome)
    {
        Nome = nome;
    }

    public void AlterarPeriodo(int periodo)
    {
        Periodo = periodo;
    }

    public void AlterarModoDeUso(string modoDeUso)
    {
        ModoDeUso = modoDeUso;
    }

    public void Validar()
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(Nome), Resource.NomeInvalido)
            .Quando(Periodo <= 0, Resource.PeriodoInvalido)
            .Quando(ModoDeUso.Length < 3, Resource.ModoDeUsoInvalido)
            .DispararExcecaoSeExistir();
    }
}
