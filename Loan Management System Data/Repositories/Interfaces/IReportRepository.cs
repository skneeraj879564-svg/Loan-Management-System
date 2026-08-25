using Loan_Management_System_Data.Models;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<List<LoanApplication>> GetLoanReportsAsync();

        Task<List<Payment>> GetCollectionReportsAsync();

        Task<List<LoanApplication>> GetRejectionReportsAsync();

        Task<List<LoanRepayment>> GetOverdueReportsAsync();

        Task<LoanApplication?> GetCustomerStatementAsync(
            int customerId);

        Task<List<Payment>> GetPaymentHistoryAsync(
            int loanApplicationId);
    }
}