using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LoanCalculator;
using Newtonsoft.Json;

namespace LoanCalculatorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter required data.");

            var parseResult = new ParseResult();
            var sourceString = "";
            while (!string.IsNullOrWhiteSpace(sourceString = Console.ReadLine()))
            {
                if (!ConsoleInputParser.ParseInput(sourceString, parseResult))
                {
                    Console.WriteLine("Invalid input. Press any key to exit.");
                    Console.ReadKey();
                    return;
                }
            }

            if (!parseResult.IsComplete)
            {
                Console.WriteLine("Invalid input. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            var loanCalculator = new LoanCalculator.LoanCalculator();
            var loanCalculationResult = loanCalculator.CalculateAmortizingLoan(parseResult.Amount.Value, parseResult.Interest.Value / 100m, parseResult.Term.Value, parseResult.DownPayment.Value);

            // using extra object as long as "monthly payment" is only particular case of loan calculator and to leave loan calcuation single responsible
            var consoleOutPutResult = new ConsoleOutputResult(loanCalculationResult);
            Console.Write(JsonConvert.SerializeObject(consoleOutPutResult, Formatting.Indented));

            Console.ReadKey();
        }

        // helper class
        class ConsoleOutputResult
        {
            [JsonProperty("monthly payment")]
            public decimal MonthlyPayment { get; set; }

            [JsonProperty("total interest")]
            public decimal TotalInterest { get; set; }

            [JsonProperty("total payment")]
            public decimal TotalPayment { get; set; }

            // assuming that result for 12 periodic payments per year is passed
            public ConsoleOutputResult(AmortizingLoanCalculationResult amortizingLoanCalculationResult)
            {
                MonthlyPayment = Math.Round(amortizingLoanCalculationResult.PeriodicPayment, 2);
                TotalInterest = Math.Round(amortizingLoanCalculationResult.TotalInterest, 2);
                TotalPayment = Math.Round(amortizingLoanCalculationResult.TotalPayment, 2);
            }
        }
    }
}
