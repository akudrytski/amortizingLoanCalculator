using System;
using System.Text.RegularExpressions;

namespace LoanCalculatorConsoleApp
{
    public static class ConsoleInputParser
    {
        private const string DecimalRegExTemplate = @"[\d,]*\d+(.\d{1,5})?";
        private const string IntegerRegExTemplate = @"\d+";
        private static Regex _decimalRegEx = new Regex(DecimalRegExTemplate, RegexOptions.Compiled);
        private static Regex _intRegEx = new Regex(IntegerRegExTemplate, RegexOptions.Compiled);
        private static Regex _amountStringRegEx = new Regex(@"^ *amount *: *" + DecimalRegExTemplate, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex _downPaymentStringRegEx = new Regex(@"^ *downpayment *: *" + DecimalRegExTemplate, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex _interestStringRegEx = new Regex(@"^ *interest *: *" + DecimalRegExTemplate, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex _termStringRegEx = new Regex(@"^ *term *: *" + IntegerRegExTemplate, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool ParseInput(string inputString, ParseResult parseResult)
        {
            if (parseResult == null) throw new ArgumentNullException(nameof(parseResult));

            if (string.IsNullOrWhiteSpace(inputString)) return false;

            if (_amountStringRegEx.IsMatch(inputString))
            {
                var amountString = _decimalRegEx.Match(inputString).Value;

                decimal amount;
                if (!Decimal.TryParse(amountString, out amount)) return false;

                parseResult.Amount = amount;
                return true;
            }

            if (_downPaymentStringRegEx.IsMatch(inputString))
            {
                var downPaymentString = _decimalRegEx.Match(inputString).Value;

                decimal downPayment;
                if (!Decimal.TryParse(downPaymentString, out downPayment)) return false;

                parseResult.DownPayment = downPayment;
                return true;
            }

            if (_interestStringRegEx.IsMatch(inputString))
            {
                var interestString = _decimalRegEx.Match(inputString).Value;

                decimal interest;
                if (!Decimal.TryParse(interestString, out interest)) return false;

                parseResult.Interest = interest;
                return true;
            }

            if (_termStringRegEx.IsMatch(inputString))
            {
                var termString = _intRegEx.Match(inputString).Value;

                int term;
                if (!int.TryParse(termString, out term)) return false;

                parseResult.Term = term;
                return true;
            }

            return false;
        }
    }
}