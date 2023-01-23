using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanStreet.LoanService.Api.DataContract
{
    public class LoanDetails
    {
        public LoanDetails() { }

        public LoanDetails(
            decimal amount, 
            decimal balance, 
            decimal interestRate, 
            int loanLengthInMonths,
            decimal monthlyPayment)
        {
            Amount = amount;
            Balance = balance;
            InterestRate = interestRate;
            LoanLengthInMonths = loanLengthInMonths;
            MonthlyPayment = monthlyPayment;
        }
        [Required]
        public decimal Amount { get; set; } = 0;
        [Required]
        public decimal Balance { get; set; } = 0;

        [Required]
        public decimal InterestRate { get; set; } = 0;
        [Required]
        public int LoanLengthInMonths { get; set; } = 0;
        [Required]
        public decimal MonthlyPayment { get; set; } = 0;
    }
}
