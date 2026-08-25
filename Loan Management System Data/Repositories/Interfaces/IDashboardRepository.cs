using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<int> GetTotalCustomersAsync();

        Task<int> GetTotalEmployeesAsync();

        Task<int> GetTotalLoanApplicationsAsync();

        Task<int> GetPendingLoansAsync();

        Task<int> GetApprovedLoansAsync();

        Task<int> GetRejectedLoansAsync();

        Task<int> GetTotalLoanRepaymentsAsync();

        Task<int> GetTotalPaymentsAsync();

        Task<decimal> GetTotalPaymentAmountAsync();

        Task<decimal> GetTotalOutstandingAmountAsync();
    }
}
