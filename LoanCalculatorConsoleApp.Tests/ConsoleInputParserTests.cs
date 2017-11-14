using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LoanCalculatorConsoleApp.Tests
{
    [TestFixture]
    public class ConsoleInputParserTests
    {
        private const decimal Precision = 0.0001m;
        
        private static readonly TestCaseData[] AmountDataSource =
        {
            new TestCaseData(new object[]
            {
                "amount: 100000", 100000m
            }),
            new TestCaseData(new object[]
            {
                "AmOunt : 100000", 100000m
            }),
            new TestCaseData(new object[]
            {
                "  AmOunt : 1000,00", 100000m
            }),
            new TestCaseData(new object[]
            {
                "  AmOunt : 1000,000", 1000000m
            }),
            new TestCaseData(new object[]
            {
                "  AmOunt : 1000,000.01", 1000000.01m
            }),
            new TestCaseData(new object[]
            {
                "  AmOunt : 1000000.99", 1000000.99m
            }),
        };

        // using TestCaseSource as long as TestCase attribute does not support decimal constants
        [Test, TestCaseSource("AmountDataSource")]
        public void ParseInputSetsCorrectAmountWhenInvokedWithCorrectParameters(string sourceString, decimal expectedResult)
        {
            //Arrange
            var parseResult = new ParseResult();

            //Act
            var result = ConsoleInputParser.ParseInput(sourceString, parseResult);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(parseResult.Amount.HasValue);
            Assert.IsTrue(Math.Abs(expectedResult - parseResult.Amount.Value) <= Precision);
        }

        private static readonly TestCaseData[] DownPaymentDataSource =
        {
            new TestCaseData(new object[]
            {
                "downpayment: 100000", 100000m
            }),
            new TestCaseData(new object[]
            {
                "DownPayment : 100000", 100000m
            }),
            new TestCaseData(new object[]
            {
                "  DownPayment : 1000,00", 100000m
            }),
            new TestCaseData(new object[]
            {
                "  DownPayment : 1000,000", 1000000m
            }),
            new TestCaseData(new object[]
            {
                "  DownPayment : 1000,000.01", 1000000.01m
            }),
            new TestCaseData(new object[]
            {
                "  DownPayment : 1000000.99", 1000000.99m
            }),
        };

        // using TestCaseSource as long as TestCase attribute does not support decimal constants
        [Test, TestCaseSource("DownPaymentDataSource")]
        public void ParseInputSetsCorrectDownPaymentWhenInvokedWithCorrectParameters(string sourceString, decimal expectedResult)
        {
            //Arrange
            var parseResult = new ParseResult();

            //Act
            var result = ConsoleInputParser.ParseInput(sourceString, parseResult);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(parseResult.DownPayment.HasValue);
            Assert.IsTrue(Math.Abs(expectedResult - parseResult.DownPayment.Value) <= Precision);
        }

        private static readonly TestCaseData[] InterestDataSource =
        {
            new TestCaseData(new object[]
            {
                "interest: 100000", 100000m
            }),
            new TestCaseData(new object[]
            {
                "InTerest : 100000", 100000m
            }),
            new TestCaseData(new object[]
            {
                "  InTerest : 1000,00", 100000m
            }),
            new TestCaseData(new object[]
            {
                "  InTerest : 1000,000", 1000000m
            }),
            new TestCaseData(new object[]
            {
                "  InTerest : 1000,000.01", 1000000.01m
            }),
            new TestCaseData(new object[]
            {
                "  InTerest : 1000000.99", 1000000.99m
            }),
        };

        // using TestCaseSource as long as TestCase attribute does not support decimal constants
        [Test, TestCaseSource("InterestDataSource")]
        public void ParseInputSetsCorrectInterestWhenInvokedWithCorrectParameters(string sourceString, decimal expectedResult)
        {
            //Arrange
            var parseResult = new ParseResult();

            //Act
            var result = ConsoleInputParser.ParseInput(sourceString, parseResult);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(parseResult.Interest.HasValue);
            Assert.IsTrue(Math.Abs(expectedResult - parseResult.Interest.Value) <= Precision);
        }

        [TestCase("term: 100000", 100000)]
        [TestCase("tERm : 100000", 100000)]
        public void ParseInputSetsCorrectTermWhenInvokedWithCorrectParameters(string sourceString, decimal expectedResult)
        {
            //Arrange
            var parseResult = new ParseResult();

            //Act
            var result = ConsoleInputParser.ParseInput(sourceString, parseResult);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(parseResult.Term.HasValue);
            Assert.AreEqual(expectedResult, parseResult.Term.Value);
        }
    }
}
