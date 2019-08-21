using System;
using System.Linq;

namespace Business.Models.Validations.Documents
{
    public class CPFValidation
    {
        public const int CPFLenght = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumeros = Utils.OnlyNumbers(cpf);

            if (!TamanhoValido(cpfNumeros)) return false;
            return !TemDigitosRepetidos(cpfNumeros) && TemDigitosValidos(cpfNumeros);
        }

        private static bool TamanhoValido(string valor)
        {
            return valor.Length == CPFLenght;
        }

        private static bool TemDigitosRepetidos(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool TemDigitosValidos(string valor)
        {
            var number = valor.Substring(0, CPFLenght - 2);
            var digitoVerificador = new Utils(number)
                .WithMultiplicatorsFromUntil(2, 11)
                .Substitution("0", 10, 11);
            var firstDigit = digitoVerificador.CalculateDigit();
            digitoVerificador.AddDigito(firstDigit);
            var secondDigit = digitoVerificador.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CPFLenght - 2, 2);
        }
    }
}
