namespace Hospital.Dominio.Base;

public class ValidaRegistrosOficiais
{
    public string CPF { get; set; }
    public string CRM { get; set; }

    public ValidaRegistrosOficiais(string cpf, string crm)
    {
        CPF = cpf;
        CRM = crm;
    }

    public static bool ValidateCPF(string cpf)
    {
        // Check if the CPF has 11 digits
        if (cpf.Length != 11)
            return false;

        // Check for known invalid CPFs
        if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" ||
            cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" ||
            cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" ||
            cpf == "99999999999")
            return false;

        int[] digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        // Validate the first digit
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (10 - i) * digits[i];
        int remainder = sum % 11;
        int expectedDigit1 = (remainder < 2) ? 0 : 11 - remainder;

        if (digits[9] != expectedDigit1)
            return false;

        // Validate the second digit
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (11 - i) * digits[i];
        remainder = sum % 11;
        int expectedDigit2 = (remainder < 2) ? 0 : 11 - remainder;

        if (digits[10] != expectedDigit2)
            return false;

        return true;
    }

    public static bool ValidateCRM(string crm)
    {
        // Check if the CRM has at least 2 letters plus 5 numbers 
        if (crm.Length < 7)
            return false;

        string statePrefix = crm.Substring(0, 2);

        // Check if the state prefix is valid (this is a simplified example)
        List<string> validStatePrefixes = new List<string> { "SP", "RJ", "MG", "RS", "PR", "SC", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "PA", "PB", "PE", "PI", "RN", "RO", "RR", "SE", "TO", "AM", "AP", "AC", "AL" };

        if (!validStatePrefixes.Contains(statePrefix.ToUpper()))
            return false;

        // Check the remaining digits for validity (you may need to implement specific rules)
        string numericPart = crm.Substring(2);

        int numericValue;
        if (!int.TryParse(numericPart, out numericValue))
            return false;

        // Additional validation rules can be implemented here based on specific requirements

        return true;
    }
}
