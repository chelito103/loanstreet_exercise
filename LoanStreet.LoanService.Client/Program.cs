// See https://aka.ms/new-console-template for more information

using LoanService.DataModel;
using LoanStreet.LoanService.Api.DataContract;
using LoanStreet.LoanService.Client;
using Refit;
Random random = new Random(DateTime.Now.Second);
Console.WriteLine("LoanStreet Loan Client App");

var loanApi = RestService.For<ILoanApi>("https://mitut76gzd.execute-api.us-east-2.amazonaws.com/Prod");

var loans = await loanApi.GetAllAsync();

Console.WriteLine("Fetching all Loans currently in the system.");
loans.ForEach(PrintLoan);

Console.WriteLine("\n\nCreating new loan");
var newLoan = new LoanDetails(12, 100, 10, 12, 10);
Guid id = await loanApi.CreateAsync(newLoan);

Console.WriteLine($"\n\nNew loan id returned is {id}");

Console.WriteLine($"\n\nRetrieving new loan with id {id} to display");
var retrievedLoan = await loanApi.GetByIdAsync(id);

PrintLoan(retrievedLoan);

int newAmount = random.Next();
Console.WriteLine($"\n\nUpdating the amount to ${newAmount}");

retrievedLoan.LoanDetails.Amount = newAmount;
await loanApi.UpdateAsync(retrievedLoan);
//System.Threading.Thread.Sleep(5000);

loans = await loanApi.GetAllAsync();
Console.WriteLine("\n\nFetching all Loans currently in the system.");
loans.ForEach(PrintLoan);



void PrintLoan(Loan loan)
{
    Console.WriteLine($"Loan ID = {loan.Id}");
    PrintLoanDetails(loan.LoanDetails);
}

void PrintLoanDetails(LoanDetails loanDetails)
{
    Console.WriteLine($"\tAmount = {loanDetails.Amount}");
    Console.WriteLine($"\tBalance = {loanDetails.Balance}");
    Console.WriteLine($"\tInterestRate = {loanDetails.InterestRate}");
    Console.WriteLine($"\tLoanLengthInMonths = {loanDetails.LoanLengthInMonths}");
    Console.WriteLine($"\tMonthlyPayment = {loanDetails.MonthlyPayment}");
}