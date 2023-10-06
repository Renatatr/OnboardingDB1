namespace Hospital.Dominio.Base;

public class ValidadorDeRegra
{
    private readonly List<string> _mensagensDeErros;

    private ValidadorDeRegra()
    {
        _mensagensDeErros = new List<string>();
    }

    public static ValidadorDeRegra Novo()
    {
        return new ValidadorDeRegra();
    }

    public ValidadorDeRegra Quando(bool temErro, string mensagemDeErro)
    {
        if (temErro)
            _mensagensDeErros.Add(mensagemDeErro);

        return this;
    }

    public void DispararExcecaoSeExistir()
    {
        if (_mensagensDeErros.Any())
            throw new ExcecaoDeDominio(_mensagensDeErros);
    }
}

public class ExcecaoDeDominio : ArgumentException
{
    public List<string> MensagensDeErro { get; set; }

    public ExcecaoDeDominio(List<string> mensagensDeErros)
    {
        MensagensDeErro = mensagensDeErros;
    }

    public override string Message
    {
        get
        {
            return GetMessage(this.MensagensDeErro);
        }
    }

    private string GetMessage(List<string> mensagens)
    {
        var menssagem = "";
        mensagens.ForEach(x => menssagem += x + ", ");
        return menssagem;
    }
}
