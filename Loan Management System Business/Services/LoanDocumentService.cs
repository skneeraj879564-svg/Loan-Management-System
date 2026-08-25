//using Loan_Management_System_Business.Dtos.LoanDocument;
//using Loan_Management_System_Business.Interfaces;
//using Loan_Management_System_Data.Models;
//using Loan_Management_System_Data.Repositories.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Loan_Management_System_Business.Services
//{
//    public class LoanDocumentService: ILoanDocumentService
//    {
//        private readonly ILoanDocumentRepository _repository;

//        public LoanDocumentService(
//            ILoanDocumentRepository repository)
//        {
//            _repository = repository;
//        }

//        // =========================
//        // GET BY ID
//        // =========================

//        public async Task<LoanDocumentResponseDto?> GetByIdAsync(
//            int loanDocumentId)
//        {
//            var document =
//                await _repository.GetByIdAsync(
//                    loanDocumentId);

//            if (document == null)
//                return null;

//            return MapToResponseDto(document);
//        }

//        // =========================
//        // GET ALL
//        // =========================

//        public async Task<List<LoanDocumentResponseDto>>
//            GetAllAsync()
//        {
//            var documents =
//                await _repository.GetAllAsync();

//            return documents
//                .Select(MapToResponseDto)
//                .ToList();
//        }

//        // =========================
//        // GET BY LOAN APPLICATION
//        // =========================

//        public async Task<List<LoanDocumentResponseDto>>
//            GetByLoanApplicationIdAsync(
//                int loanApplicationId)
//        {
//            var documents =
//                await _repository
//                    .GetByLoanApplicationIdAsync(
//                        loanApplicationId);

//            return documents
//                .Select(MapToResponseDto)
//                .ToList();
//        }

//        // =========================
//        // CREATE
//        // =========================

//        public async Task<LoanDocumentResponseDto>
//            CreateAsync(CreateLoanDocumentDto model)
//        {
//            var document = new LoanDocument
//            {
//                LoanApplicationId =
//                    model.LoanApplicationId,

//                DocumentType =
//                    model.DocumentType,

//                DocumentName =
//                    model.DocumentName,

//                FilePath =
//                    model.FilePath,

//                UploadedDate =
//                    DateTime.UtcNow,

//                VerificationStatus =
//                    "Pending",

//                VerificationRemarks = null,

//                VerifiedByEmployeeId = null,

//                VerifiedDate = null
//            };

//            var result =
//                await _repository.AddAsync(document);

//            return MapToResponseDto(result);
//        }

//        // =========================
//        // UPDATE
//        // =========================

//        public async Task<LoanDocumentResponseDto?>
//            UpdateAsync(
//                int loanDocumentId,
//                UpdateLoanDocumentDto model)
//        {
//            var document =
//                await _repository.GetByIdAsync(
//                    loanDocumentId);

//            if (document == null)
//                return null;

//            document.DocumentType =
//                model.DocumentType;

//            document.DocumentName =
//                model.DocumentName;

//            document.FilePath =
//                model.FilePath;

//            document.VerificationStatus =
//                model.VerificationStatus;

//            document.VerificationRemarks =
//                model.VerificationRemarks;

//            document.VerifiedByEmployeeId =
//                model.VerifiedByEmployeeId;

//            document.VerifiedDate =
//                model.VerifiedDate;

//            var result =
//                await _repository.UpdateAsync(
//                    document);

//            return MapToResponseDto(result);
//        }

//        // =========================
//        // DELETE
//        // =========================

//        public async Task<bool>
//            DeleteAsync(int loanDocumentId)
//        {
//            return await _repository.DeleteAsync(
//                loanDocumentId);
//        }

//        // =========================
//        // MAPPING
//        // =========================

//        private static LoanDocumentResponseDto
//            MapToResponseDto(
//                LoanDocument document)
//        {
//            return new LoanDocumentResponseDto
//            {
//                LoanDocumentId =
//                    document.LoanDocumentId,

//                LoanApplicationId =
//                    document.LoanApplicationId,

//                DocumentType =
//                    document.DocumentType,

//                DocumentName =
//                    document.DocumentName,

//                FilePath =
//                    document.FilePath,

//                UploadedDate =
//                    document.UploadedDate,

//                VerificationStatus =
//                    document.VerificationStatus,

//                VerificationRemarks =
//                    document.VerificationRemarks,

//                VerifiedByEmployeeId =
//                    document.VerifiedByEmployeeId,

//                VerifiedDate =
//                    document.VerifiedDate
//            };
//        }
//    }
//}
using Loan_Management_System_Business.Dtos.LoanDocument;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;

namespace Loan_Management_System_Business.Services
{
    public class LoanDocumentService : ILoanDocumentService
    {
        private readonly ILoanDocumentRepository _repository;

        public LoanDocumentService(
            ILoanDocumentRepository repository)
        {
            _repository = repository;
        }


        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanDocumentResponseDto?> GetByIdAsync(
            int loanDocumentId)
        {
            var document =
                await _repository.GetByIdAsync(
                    loanDocumentId);

            if (document == null)
            {
                return null;
            }

            return MapToResponseDto(document);
        }


        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanDocumentResponseDto>>
            GetAllAsync()
        {
            var documents =
                await _repository.GetAllAsync();

            return documents
                .Select(MapToResponseDto)
                .ToList();
        }


        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<LoanDocumentResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            var documents =
                await _repository
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            return documents
                .Select(MapToResponseDto)
                .ToList();
        }


        // =========================
        // CREATE / UPLOAD DOCUMENT
        // =========================

        public async Task<LoanDocumentResponseDto>
            CreateAsync(
                CreateLoanDocumentDto model,
                string documentName,
                string storedFileName,
                string filePath,
                string? contentType,
                long fileSize)
        {
            var document = new LoanDocument
            {
                LoanApplicationId =
                    model.LoanApplicationId,

                DocumentType =
                    model.DocumentType,

                DocumentName =
                    documentName,

                StoredFileName =
                    storedFileName,

                FilePath =
                    filePath,

                ContentType =
                    contentType,

                FileSize =
                    fileSize,

                UploadedDate =
                    DateTime.UtcNow,

                VerificationStatus =
                    "Pending",

                VerificationRemarks =
                    null,

                VerifiedByEmployeeId =
                    null,

                VerifiedDate =
                    null
            };

            var result =
                await _repository.AddAsync(
                    document);

            return MapToResponseDto(result);
        }


        // =========================
        // UPDATE / VERIFY DOCUMENT
        // =========================

        public async Task<LoanDocumentResponseDto?>
            UpdateAsync(
                int loanDocumentId,
                UpdateLoanDocumentDto model)
        {
            var document =
                await _repository.GetByIdAsync(
                    loanDocumentId);

            if (document == null)
            {
                return null;
            }

            document.DocumentType =
                model.DocumentType;

            document.VerificationStatus =
                model.VerificationStatus;

            document.VerificationRemarks =
                model.VerificationRemarks;

            document.VerifiedByEmployeeId =
                model.VerifiedByEmployeeId;

            document.VerifiedDate =
                model.VerifiedDate;

            var result =
                await _repository.UpdateAsync(
                    document);

            return MapToResponseDto(result);
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool>
            DeleteAsync(
                int loanDocumentId)
        {
            return await _repository.DeleteAsync(
                loanDocumentId);
        }


        // =========================
        // MAPPING
        // =========================

        private static LoanDocumentResponseDto
            MapToResponseDto(
                LoanDocument document)
        {
            return new LoanDocumentResponseDto
            {
                LoanDocumentId =
                    document.LoanDocumentId,

                LoanApplicationId =
                    document.LoanApplicationId,

                DocumentType =
                    document.DocumentType,

                DocumentName =
                    document.DocumentName,

                StoredFileName =
                    document.StoredFileName,

                FilePath =
                    document.FilePath,

                ContentType =
                    document.ContentType,

                FileSize =
                    document.FileSize,

                UploadedDate =
                    document.UploadedDate,

                VerificationStatus =
                    document.VerificationStatus,

                VerificationRemarks =
                    document.VerificationRemarks,

                VerifiedByEmployeeId =
                    document.VerifiedByEmployeeId,

                VerifiedDate =
                    document.VerifiedDate
            };
        }
    }
}
