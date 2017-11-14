using System;
using NUnit.Framework;

namespace LoanCalculator.Tests
{
    [TestFixture]
    public class LoanCalculatorTests
    {
        private const decimal Precision = 0.0001m;

        private static readonly TestCaseData[] CalculateAmortizingLoanDataSource =
        {
            new TestCaseData(new object[]
            {
                100000m, 0.055m, 30, 20000m, 454.231201m, 83523.232387m, 163523.232388m
            }),
            new TestCaseData(new object[]
            {
                100000m, 0.055m, 30, 0m, 567.789001m, 104404.040485m, 204404.040485m
            }),
            new TestCaseData(new object[]
            {
                1m, 0.055m, 30, 0m, 0.005678m, 1.044040m, 2.044040m
            }),
            new TestCaseData(new object[]
            {
                100000m, 1.1m, 30, 0m, 9166.666667m, 3200000.000000m, 3300000.000000m
            }),
            new TestCaseData(new object[]
            {
                100000m, 1.1m, 30, 100000m, 0m, 0m, 100000m
            }),
        };

        // using TestCaseSource as long as TestCase attribute does not support decimal constants
        [Test, TestCaseSource("CalculateAmortizingLoanDataSource")]
        public void CalculateAmortizingLoanReturnsCorrectValueWhenInvoked(
            decimal amount, decimal interest, int termInYears, decimal downPayment, 
            decimal expectedMonthlyPayment, decimal expectedTotalInterest, decimal expectedTotalPayment)
        {
            //Arrange
            var loanCalculator = new LoanCalculator();

            //Act
            var result = loanCalculator.CalculateAmortizingLoan(amount, interest, termInYears, downPayment);

            TestContext.WriteLine(result.PeriodicPayment);
            TestContext.WriteLine(result.TotalInterest);
            TestContext.WriteLine(result.TotalPayment);

            TestContext.WriteLine(Math.Abs(expectedMonthlyPayment - result.PeriodicPayment));
            TestContext.WriteLine(Math.Abs(expectedTotalInterest - result.TotalInterest));
            TestContext.WriteLine(Math.Abs(expectedTotalPayment - result.TotalPayment));

            //Assert
            Assert.IsTrue(Math.Abs(expectedMonthlyPayment - result.PeriodicPayment) <= Precision);
            Assert.IsTrue(Math.Abs(expectedTotalInterest - result.TotalInterest) <= Precision);
            Assert.IsTrue(Math.Abs(expectedTotalPayment - result.TotalPayment) <= Precision);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void CalculateAmortizingLoanThrowsArgumentOutOfRangeExceptionWhenAmountIsOutOfRange(decimal amount)
        {
            //Arrange
            var loanCalculator = new LoanCalculator();

            //Act
            //Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => loanCalculator.CalculateAmortizingLoan(amount, 0.5m, 30));
            Assert.AreEqual(ex.ParamName, "amount");
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void CalculateAmortizingLoanThrowsArgumentOutOfRangeExceptionWhenInterestIsOutOfRange(decimal interest)
        {
            //Arrange
            var loanCalculator = new LoanCalculator();

            //Act
            //Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => loanCalculator.CalculateAmortizingLoan(100m, interest, 30));
            Assert.AreEqual(ex.ParamName, "interest");
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void CalculateAmortizingLoanThrowsArgumentOutOfRangeExceptionWhenTermInYearsIsOutOfRange(int termInYears)
        {
            //Arrange
            var loanCalculator = new LoanCalculator();

            //Act
            //Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => loanCalculator.CalculateAmortizingLoan(100m, 0.55m, termInYears));
            Assert.AreEqual(ex.ParamName, "termInYears");
        }

        [Test]
        public void CalculateAmortizingLoanThrowsArgumentOutOfRangeExceptionWhenDownPaymentIsOutOfRange()
        {
            //Arrange
            var loanCalculator = new LoanCalculator();

            //Act
            //Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => loanCalculator.CalculateAmortizingLoan(100m, 0.55m, 12, -1m));
            Assert.AreEqual(ex.ParamName, "downPayment");
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void CalculateAmortizingLoanThrowsArgumentOutOfRangeExceptionWhenNumberOfPaymentsPerYearIsOutOfRange(int numberOfPaymentsPerYear)
        {
            //Arrange
            var loanCalculator = new LoanCalculator();

            //Act
            //Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => loanCalculator.CalculateAmortizingLoan(100m, 0.55m, 12, 0m, numberOfPaymentsPerYear));
            Assert.AreEqual(ex.ParamName, "numberOfPaymentsPerYear");
        }
    }
}