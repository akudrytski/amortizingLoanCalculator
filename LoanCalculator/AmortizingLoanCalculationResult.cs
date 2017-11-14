namespace LoanCalculator
{
    public class AmortizingLoanCalculationResult
    {
        public decimal PeriodicPayment { get; }
        public decimal TotalInterest { get; }
        public decimal TotalPayment { get; }

        public AmortizingLoanCalculationResult(decimal periodicPayment, decimal totalInterest, decimal totalPayment)
        {
            PeriodicPayment = periodicPayment;
            TotalInterest = totalInterest;
            TotalPayment = totalPayment;
        }
    }
}