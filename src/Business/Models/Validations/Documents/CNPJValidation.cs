using System;
using System.Linq;

namespace Business.Models.Validations.Documents
{
    public class CNPJValidation
    {
        public const int CNPJLength = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumeros = Utils.OnlyNumbers(cpnj);

            if (!TemTamanhoValido(cnpjNumeros)) return false;
            return !TemDigitosRepetidos(cnpjNumeros) && TemDigitosValidos(cnpjNumeros);
        }

        private static bool TemTamanhoValido(string valor)
        {
            return valor.Length == CNPJLength;
        }

        private static bool TemDigitosRepetidos(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool TemDigitosValidos(string valor)
        {
            var number = valor.Substring(0, CNPJLength - 2);

            var digitoVerificador = new Utils(number)
                .WithMultiplicatorsFromUntil(2, 9)
                .Substitution("0", 10, 11);
            var firstDigit = digitoVerificador.CalculateDigit();
            digitoVerificador.AddDigito(firstDigit);
            var secondDigit = digitoVerificador.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CNPJLength - 2, 2);
        }
    }
}
