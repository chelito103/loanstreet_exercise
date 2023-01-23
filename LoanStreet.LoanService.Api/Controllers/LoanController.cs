using LoanService.DataModel;
using LoanStreet.LoanService.Api.DataContract;
using LoanStreet.LoanService.Repository.Loan;
using Microsoft.AspNetCore.Mvc;
using Loan = LoanService.DataModel.Loan;

namespace LoanService.Controllers
{
    /// <summary>
    /// Endpoint for creating/managing/viewing loans.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;
        private readonly LoanRepository _loanRepository;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public LoanController(ILogger<LoanController> logger, LoanRepository loanRepository)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _logger = logger;
            _loanRepository = loanRepository;
        }

        /// <summary>
        /// Returns individual loan requested by Id, or all loans.
        /// </summary>
        /// <param name="loanId">When provided endpoint returns 1 loan based on this Id.</param>
        /// <returns>Loan(s) based on parameters</returns>
        [HttpGet]
        public async Task<IActionResult> GetLoanAsync(
            [FromQuery] Guid? loanId)
        {
            _logger.LogTrace($"Entering GetLoanAsync endpoint");
            if (loanId.HasValue)
            {
                var loan = ConvertRepoLoanToContract(await _loanRepository.GetByIdAsync(loanId.Value));

                if (loan == null)
                {
                    return NotFound();
                }
                return Ok(loan);
            }

            var repoLoans = await _loanRepository.GetAllAsync();
            var loans = repoLoans.Select(ConvertRepoLoanToContract).ToList();

            _logger.LogTrace($"Exited GetLoanAsync endpoint");
            return Ok(loans);
        }

        /// <summary>
        /// Create/update a loan object
        /// </summary>
        /// <param name="loanDetails"></param>
        /// <returns>Status Code 200 on success.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateLoanAsync([FromBody] LoanDetails loanDetails)
        {
            _logger.LogTrace($"Entering UpsertLoanAsync endpoint");
            Guid rv;
            if (ModelState.IsValid)
            {
                var loan = new Loan(Guid.NewGuid(), loanDetails);
                rv = await _loanRepository.UpsertAsync(ConvertContractToRepoLoan(loan));
            }
            else
            {
                return BadRequest("Parameters invalid: Requires an object of loan model.");
            }
            
            _logger.LogTrace($"Exited UpsertLoanAsync endpoint");
            return Ok(rv);
        }

        /// <summary>
        /// Create/update a loan object
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>Status Code 200 on success.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLoanAsync([FromBody] Loan loan)
        {
            _logger.LogTrace($"Entering UpsertLoanAsync endpoint");
            if (ModelState.IsValid)
            {
                var repoLoan = await _loanRepository.GetByIdAsync(loan.Id);
                if (repoLoan != null)
                {
                    await _loanRepository.UpsertAsync(ConvertContractToRepoLoan(loan));
                    return Ok();
                }

                return BadRequest($"Loan with Id = {loan.Id} does not in exist.");
            }

            _logger.LogTrace($"Exited UpsertLoanAsync endpoint");
            return BadRequest("Parameters invalid: Requires an object of loan model.");
        }

        private Loan? ConvertRepoLoanToContract(LoanStreet.LoanService.Repository.Loan.Loan? repoLoan)
        {
            return (repoLoan == null) ? null: new Loan(
                repoLoan.Id,
                new LoanDetails(
                    repoLoan.Amount, 
                    repoLoan.Balance, 
                    repoLoan.InterestRate, 
                    repoLoan.LoanLengthInMonths,
                    repoLoan.MonthlyPayment));
        }

        private LoanStreet.LoanService.Repository.Loan.Loan ConvertContractToRepoLoan(Loan loan)
        {
            return new LoanStreet.LoanService.Repository.Loan.Loan()
            {
                Id = loan.Id,
                Amount = loan.LoanDetails.Amount,
                Balance = loan.LoanDetails.Balance,
                InterestRate = loan.LoanDetails.InterestRate,
                LoanLengthInMonths = loan.LoanDetails.LoanLengthInMonths,
                MonthlyPayment = loan.LoanDetails.MonthlyPayment
            };
        }
    }
}