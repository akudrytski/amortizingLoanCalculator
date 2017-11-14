using System;

namespace LoanCalculator
{
    public class LoanCalculator
    {
        /// <summary>
        /// Calculates amortizing loan periodic payment (default period is month), total interest, total payment.
        /// </summary>
        /// <param name="amount">Total amount</param>
        /// <param name="interest">Interest represented in decimal format. Greater than 0 less than 1.</param>
        /// <param name="termInYears">Term in years.</param>
        /// <param name="downPayment">Down payment. Default is zero.</param>
        /// <param name="numberOfPaymentsPerYear">Number of payments per year. Default is 12 times, which results in period equal to month.</param>
        /// <returns>AmortizingLoanCalculationResult instance.</returns>
        public AmortizingLoanCalculationResult CalculateAmortizingLoan(decimal amount, decimal interest, int termInYears, decimal downPayment = 0m, int numberOfPaymentsPerYear = 12)
        {
            if (amount <= 0m) throw new ArgumentOutOfRangeException(nameof(amount));
            if (interest <= 0m) throw new ArgumentOutOfRangeException(nameof(interest)); // we allow interest to be greater than 100%
            if (termInYears <= 0) throw new ArgumentOutOfRangeException(nameof(termInYears));
            if (downPayment < 0) throw new ArgumentOutOfRangeException(nameof(downPayment));
            if (numberOfPaymentsPerYear <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfPaymentsPerYear));

            if (amount == downPayment)
            {
                return new AmortizingLoanCalculationResult(0m, 0m, downPayment);
            }

            var loanAmount = amount - downPayment;
            var numberOfPeriodicPayments = termInYears * numberOfPaymentsPerYear;

            // in order to avoid overflow exceptoin on decimal values in high power,
            // we can use double value, yet not losing the desired result precision
            var periodicInterestRate = (double) interest / numberOfPaymentsPerYear; 
            var calculatedPower = Math.Pow(1 + periodicInterestRate, numberOfPeriodicPayments);
            var discountFactor = (decimal) ((calculatedPower - 1) / calculatedPower / periodicInterestRate);

            var periodicPayment = loanAmount / discountFactor;

            var totalPayment = periodicPayment * numberOfPeriodicPayments;

            return new AmortizingLoanCalculationResult(periodicPayment, totalPayment - loanAmount, totalPayment);
        }
    }
}