using System.ComponentModel.DataAnnotations;
using LoanStreet.LoanService.Api.DataContract;

namespace LoanService.DataModel
{
    public class Loan
    {
        public Loan(Guid id, LoanDetails loanDetails)
        {
            Id = id;
            LoanDetails = loanDetails;
        }

        public Guid Id { get; set; }

        public LoanDetails LoanDetails { get; set; }
    }
}