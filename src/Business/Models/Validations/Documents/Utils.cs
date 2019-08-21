using System.Collections.Generic;

namespace Business.Models.Validations.Documents
{
    public class Utils
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _substitutions = new Dictionary<int, string>();
        private bool _complementModule = true;

        public Utils(string number)
        {
            _number = number;
        }

        public Utils WithMultiplicatorsFromUntil(int firstMultiplicator, int lastMultiplicator)
        {
            _multipliers.Clear();
            for (var i = firstMultiplicator; i <= lastMultiplicator; i++)
                _multipliers.Add(i);

            return this;
        }

        public Utils Substitution(string substitute, params int[] digits)
        {
            foreach (var i in digits)
            {
                _substitutions[i] = substitute;
            }
            return this;
        }

        public void AddDigito(string digit)
        {
            _number = string.Concat(_number, digit);
        }

        public string CalculateDigit()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var soma = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                soma += produto;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (soma % Module);
            var resultado = _complementModule ? Module - mod : mod;

            return _substitutions.ContainsKey(resultado) ? _substitutions[resultado] : resultado.ToString();
        }

        public static string OnlyNumbers(string valor)
        {
            var onlyNumber = "";
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}
