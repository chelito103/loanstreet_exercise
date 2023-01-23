using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using LoanStreet.LoanService.Repository.Loan.Impl.DyamoDbModels;
using Microsoft.Extensions.Logging;

namespace LoanStreet.LoanService.Repository.Loan.Impl
{
    public class LoanRepositoryImpl : LoanRepository
    {
        private static readonly AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private readonly ILogger<LoanRepository> _logger;

        public LoanRepositoryImpl(ILogger<LoanRepository> logger)
        {
            _logger = logger;
        }


        public async Task<IList<Loan>> GetAllAsync()
        {
            IList<Loan> loans;

            try
            {
                DynamoDBContext context = new DynamoDBContext(client);
                var conditions = new List<ScanCondition>();
                IList<DynamoLoan> dynoLoans = await context.ScanAsync<DynamoLoan>(conditions).GetRemainingAsync();
                loans = dynoLoans.Select(ConvertDynamoDbLoanToContract).ToList();
            }
            catch (AmazonDynamoDBException e) { 
                _logger.LogError(e,"Failed to retrieve all loans from DynamoDB");
                throw;
            }
            catch (AmazonServiceException e) {
                _logger.LogError(e,e.Message);
                throw;
            }

            return loans;
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            Loan? loan;
            try
            {
                DynamoDBContext context = new DynamoDBContext(client);
                DynamoLoan dynoLoan = await context.LoadAsync<DynamoLoan>(id);
                loan = ConvertDynamoDbLoanToContract(dynoLoan);
            }
            catch (AmazonDynamoDBException e)
            {
                _logger.LogError(e, "Failed to retrieve all loans from DynamoDB");
                throw;
            }
            catch (AmazonServiceException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }

            return loan;
        }

        public async Task<Guid> UpsertAsync(Loan loan)
        {
            try
            {
                DynamoDBContext context = new DynamoDBContext(client);
                DynamoLoan dynoLoan = ConvertContractLoanToDynamoDb(loan);
                await context.SaveAsync(dynoLoan);
            }
            catch (AmazonDynamoDBException e)
            {
                _logger.LogError(e, "Failed to retrieve all loans from DynamoDB");
                throw;
            }
            catch (AmazonServiceException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }

            return loan.Id;
        }

        //public async Task UpdateLoan(Loan loan)
        //{
        //    try
        //    {
        //        DynamoDBContext context = new DynamoDBContext(client);
        //        DynamoLoan dynoLoan = ConvertContractLoanToDynamoDb(loan);
        //        await context.SaveAsync(dynoLoan);
        //    }
        //    catch (AmazonDynamoDBException e)
        //    {
        //        _logger.Error(e, "Failed to retrieve all loans from DynamoDB");
        //        throw;
        //    }
        //    catch (AmazonServiceException e)
        //    {
        //        _logger.Error(e, e.Message);
        //        throw;
        //    }

        //    return;
        //}

        private Loan? ConvertDynamoDbLoanToContract(DynamoLoan dynamoLoan)
        {
            return dynamoLoan != null ? new Loan()
            {
                Id = dynamoLoan.Id, 
                Amount = dynamoLoan.Amount, 
                Balance = dynamoLoan.Balance,
                InterestRate = dynamoLoan.InterestRate, 
                LoanLengthInMonths = dynamoLoan.LoanLengthInMonths,
                MonthlyPayment = dynamoLoan.MonthlyPayment
            }: null;
        }

        private DynamoLoan ConvertContractLoanToDynamoDb(Loan loan)
        {
            return new DynamoLoan()
            {
                Id = loan.Id,
                Amount = loan.Amount,
                Balance = loan.Balance,
                InterestRate = loan.InterestRate,
                LoanLengthInMonths = loan.LoanLengthInMonths,
                MonthlyPayment = loan.MonthlyPayment
            };
        }
    }
}