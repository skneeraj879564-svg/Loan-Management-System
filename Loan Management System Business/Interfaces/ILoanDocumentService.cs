//using Loan_Management_System_Business.Dtos.LoanDocument;

//namespace Loan_Management_System_Business.Interfaces
//{
//    public interface ILoanDocumentService
//    {
//        Task<LoanDocumentResponseDto?> GetByIdAsync(
//            int loanDocumentId);

//        Task<List<LoanDocumentResponseDto>> GetAllAsync();

//        Task<List<LoanDocumentResponseDto>>
//            GetByLoanApplicationIdAsync(
//                int loanApplicationId);

//        Task<LoanDocumentResponseDto> CreateAsync(
//            CreateLoanDocumentDto model);

//        Task<LoanDocumentResponseDto?> UpdateAsync(
//            int loanDocumentId,
//            UpdateLoanDocumentDto model);

//        Task<bool> DeleteAsync(
//            int loanDocumentId);
//    }
//}
using Loan_Management_System_Business.Dtos.LoanDocument;

namespace Loan_Management_System_Business.Interfaces
{
    public interface ILoanDocumentService
    {
        // =========================
        // GET BY ID
        // =========================

        Task<LoanDocumentResponseDto?> GetByIdAsync(
            int loanDocumentId);


        // =========================
        // GET ALL
        // =========================

        Task<List<LoanDocumentResponseDto>> GetAllAsync();


        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        Task<List<LoanDocumentResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);


        // =========================
        // CREATE DOCUMENT
        // =========================

        Task<LoanDocumentResponseDto> CreateAsync(
            CreateLoanDocumentDto model,
            string documentName,
            string storedFileName,
            string filePath,
            string? contentType,
            long fileSize);


        // =========================
        // UPDATE / VERIFY DOCUMENT
        // =========================

        Task<LoanDocumentResponseDto?> UpdateAsync(
            int loanDocumentId,
            UpdateLoanDocumentDto model);


        // =========================
        // DELETE
        // =========================

        Task<bool> DeleteAsync(
            int loanDocumentId);
    }
}