# Amortizing loan calculator

## Into
This is one of the test tasks I was asked to solve as a part of hiring process.
## Task Description
You are in charge of creating a loan payment calculator.  A lot of people want to figure out given a loan, downpayment and interest rate what their payments are going to look like and how much of them is going to go towards principle vs interest.  You program is going to get input from the command line in the following format:

```
amount: 100000
interest: 5.5%
downpayment: 20000
term: 30

```
NOTE: the last line of input is a blank line.

Your program needs to process this input including the best way to handle some human errors (upper/lower case, spacing etc) and output a JSON of payment and total interest paid.

```
{
    "monthly payment": 454.23,
    "total interest": 83523.23
    "total payment" 163523.23
}
```
## How to run
Unpack, open in visual studio, restore nuget packages, run.

Program is expecting the input of the following manner(lines could have different order):
```csharp
downpayment: 20000
amount: 100000
interest: 5.5%
term: 30

```
NOTE: the last line of input is a blank line.
