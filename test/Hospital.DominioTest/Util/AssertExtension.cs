using Hospital.Dominio.Base;
using Xunit;

namespace Hospital.DominioTest.Util;

public static class AssertExtension
{
    public static void ComMensagem(this ExcecaoDeDominio exception, string mensagem)
    {
        if (exception.MensagensDeErro.Contains(mensagem))
            Assert.True(true);
        else
            Assert.False(false, $"Esperava a mensagem '{mensagem}'");
    }
}