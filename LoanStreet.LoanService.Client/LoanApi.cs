using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanService.DataModel;
using LoanStreet.LoanService.Api.DataContract;
using Refit;

namespace LoanStreet.LoanService.Client
{
    public interface ILoanApi
    {
        [Get("/loan")]
        Task<List<Loan>> GetAllAsync();

        [Get("/loan/{id}")]
        Task<Loan> GetByIdAsync(Guid id);

        [Post("/loan")]
        Task<Guid> CreateAsync([Body]LoanDetails loanDetails);

        [Put("/loan")]
        Task UpdateAsync([Body]Loan loan);
    }
}
