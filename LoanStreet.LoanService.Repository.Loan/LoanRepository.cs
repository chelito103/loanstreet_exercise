namespace LoanStreet.LoanService.Repository.Loan
{
    public interface LoanRepository
    {
        Task<IList<Loan>> GetAllAsync();

        Task<Loan?> GetByIdAsync(Guid id);

        Task<Guid> UpsertAsync(Loan loan);
    }
}