using Loan_Management_System_Business.Dtos.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IReportService
    {
        Task<List<LoanReportDto>> GetLoanReportsAsync();

        Task<List<CollectionReportDto>> GetCollectionReportsAsync();

        Task<List<RejectionReportDto>> GetRejectionReportsAsync();

        Task<List<OverdueReportDto>> GetOverdueReportsAsync();

        Task<CustomerStatementDto?> GetCustomerStatementAsync(
            int customerId);

        Task<List<PaymentHistoryDto>> GetPaymentHistoryAsync(
            int loanApplicationId);
    }
}
