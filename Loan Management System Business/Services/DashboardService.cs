using Loan_Management_System_Business.Dtos.Dashboard;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(
            IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            return new DashboardDto
            {
                TotalCustomers =
                    await _repository.GetTotalCustomersAsync(),

                TotalEmployees =
                    await _repository.GetTotalEmployeesAsync(),

                TotalLoanApplications =
                    await _repository.GetTotalLoanApplicationsAsync(),

                PendingLoans =
                    await _repository.GetPendingLoansAsync(),

                ApprovedLoans =
                    await _repository.GetApprovedLoansAsync(),

                RejectedLoans =
                    await _repository.GetRejectedLoansAsync(),

                TotalLoanRepayments =
                    await _repository.GetTotalLoanRepaymentsAsync(),

                TotalPayments =
                    await _repository.GetTotalPaymentsAsync(),

                TotalPaymentAmount =
                    await _repository.GetTotalPaymentAmountAsync(),

                TotalOutstandingAmount =
                    await _repository.GetTotalOutstandingAmountAsync()
            };
        }

    }
}
