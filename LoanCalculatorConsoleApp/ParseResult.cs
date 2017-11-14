namespace LoanCalculatorConsoleApp
{
    public class ParseResult
    {
        public decimal? Amount { get; set; }
        public decimal? Interest { get; set; }
        public decimal? DownPayment { get; set; }
        public int? Term { get; set; }

        public bool IsComplete => Amount.HasValue && Interest.HasValue && DownPayment.HasValue && Term.HasValue;
    }
}