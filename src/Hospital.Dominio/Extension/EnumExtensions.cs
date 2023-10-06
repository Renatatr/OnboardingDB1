using System.Linq;
using System.ComponentModel;

namespace Hospital.Dominio.Extension;

public static class EnumExtensions
{
    public static string ObterDescricaoFormatada<T>(this T enumerador) where T : Enum
    {
        var campo = enumerador.GetType().GetField(enumerador.ToString());
        if (campo == null) 
            return enumerador.ToString();
        
        var atributos = (DescriptionAttribute[]) campo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return atributos.Length > 0 ? atributos[0].Description : enumerador.ToString();
    }

    public static bool ContemOpcao<T>(T enumerator) where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList().Contains(enumerator);
    }
}
