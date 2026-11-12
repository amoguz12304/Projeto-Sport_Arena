public static class CpfValidator
{
    public static string Normalizar(string? cpf) =>
         new string((cpf ?? string.Empty).Where(char.IsDigit).ToArray());
    public static bool IsValid(string cpf)
    {

        if (string.IsNullOrWhiteSpace(cpf)) return false;

        var digits = new string(cpf.Where(char.IsDigit).ToArray());

        if (digits.Length != 11) return false;
        if (digits.Distinct().Count() == 1) return false;

        var numbers = digits.Select(c => c - '0').ToArray();

        return numbers[9] == CalcDigit(numbers, 9)
            && numbers[10] == CalcDigit(numbers, 10);
    }

    private static int CalcDigit(int[] n, int length)
    {
        int sum = 0;
        for (int i = 0; i < length; i++)
            sum += n[i] * (length + 1 - i);

        int rest = (sum * 10) % 11;
        return rest == 10 ? 0 : rest;
    }
}   