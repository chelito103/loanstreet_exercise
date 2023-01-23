using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanStreet.LoanService.Repository.Loan
{
    public class Loan
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; } = 0;

        public decimal Balance { get; set; } = 0;

        public decimal InterestRate { get; set; } = 0;

        public int LoanLengthInMonths { get; set; } = 0;

        public decimal MonthlyPayment { get; set; } = 0;
    }
}
