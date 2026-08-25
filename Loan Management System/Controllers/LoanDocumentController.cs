//using Loan_Management_System_Business.Dtos.LoanDocument;
//using Loan_Management_System_Business.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Loan_Management_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class LoanDocumentController : ControllerBase
//    {
//        private readonly ILoanDocumentService _service;

//        public LoanDocumentController(
//            ILoanDocumentService service)
//        {
//            _service = service;
//        }

//        // =========================
//        // GET BY ID
//        // =========================

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var result =
//                await _service.GetByIdAsync(id);

//            if (result == null)
//            {
//                return NotFound(new
//                {
//                    message = "Loan document not found."
//                });
//            }

//            return Ok(result);
//        }

//        // =========================
//        // GET ALL
//        // =========================

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var result =
//                await _service.GetAllAsync();

//            return Ok(result);
//        }

//        // =========================
//        // GET BY LOAN APPLICATION
//        // =========================

//        [HttpGet("loan-application/{loanApplicationId}")]
//        public async Task<IActionResult>
//            GetByLoanApplicationId(
//                int loanApplicationId)
//        {
//            var result =
//                await _service
//                    .GetByLoanApplicationIdAsync(
//                        loanApplicationId);

//            return Ok(result);
//        }

//        // =========================
//        // CREATE
//        // =========================

//        [HttpPost]
//        public async Task<IActionResult>
//            Create(CreateLoanDocumentDto model)
//        {
//            var result =
//                await _service.CreateAsync(model);

//            return Ok(result);
//        }

//        // =========================
//        // UPDATE
//        // =========================

//        [HttpPut("{id}")]
//        public async Task<IActionResult>
//            Update(
//                int id,
//                UpdateLoanDocumentDto model)
//        {
//            var result =
//                await _service.UpdateAsync(
//                    id,
//                    model);

//            if (result == null)
//            {
//                return NotFound(new
//                {
//                    message = "Loan document not found."
//                });
//            }

//            return Ok(result);
//        }

//        // =========================
//        // DELETE
//        // =========================

//        [HttpDelete("{id}")]
//        public async Task<IActionResult>
//            Delete(int id)
//        {
//            var result =
//                await _service.DeleteAsync(id);

//            if (!result)
//            {
//                return NotFound(new
//                {
//                    message = "Loan document not found."
//                });
//            }

//            return Ok(new
//            {
//                message = "Loan document deleted successfully."
//            });
//        }
//    }
//}
using Loan_Management_System_Business.Dtos.LoanDocument;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanDocumentController : ControllerBase
    {
        private readonly ILoanDocumentService _service;
        private readonly IWebHostEnvironment _environment;

        public LoanDocumentController(
            ILoanDocumentService service,
            IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        // =========================
        // GET BY ID
        // =========================

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Loan document not found."
                });
            }

            return Ok(result);
        }


        // =========================
        // GET ALL
        // =========================

        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer,CollectionOfficer")]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(result);
        }


        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        [HttpGet("loan-application/{loanApplicationId:int}")]
        public async Task<IActionResult>
            GetByLoanApplicationId(
                int loanApplicationId)
        {
            var result =
                await _service
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            return Ok(result);
        }


        // =========================
        // UPLOAD DOCUMENT
        // POST: api/LoanDocument/upload
        // =========================

        [HttpPost("upload")]
        [Authorize(Roles = "Admin,Customer,LoanOfficer")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(
            [FromForm] CreateLoanDocumentDto model,
            IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // =========================
            // FILE CHECK
            // =========================

            if (file == null || file.Length == 0)
            {
                return BadRequest(new
                {
                    message = "Please select a file."
                });
            }


            // =========================
            // FILE SIZE CHECK
            // Maximum 5 MB
            // =========================

            const long maxFileSize =
                5 * 1024 * 1024;

            if (file.Length > maxFileSize)
            {
                return BadRequest(new
                {
                    message =
                        "File size must not exceed 5 MB."
                });
            }


            // =========================
            // ALLOWED FILE TYPES
            // =========================

            var allowedExtensions =
                new[]
                {
                    ".pdf",
                    ".jpg",
                    ".jpeg",
                    ".png"
                };

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new
                {
                    message =
                        "Only PDF, JPG, JPEG and PNG files are allowed."
                });
            }


            // =========================
            // UPLOAD FOLDER
            // =========================
            // File will be saved here:
            //
            // Loan Management System
            // └── Uploads
            //     └── LoanDocuments
            //         └── file.pdf
            //

            var uploadFolder =
                Path.Combine(
                    _environment.ContentRootPath,
                    "Uploads",
                    "LoanDocuments");


            // =========================
            // CREATE FOLDER
            // =========================

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }


            // =========================
            // UNIQUE FILE NAME
            // =========================

            var storedFileName =
                $"{Guid.NewGuid()}{extension}";


            // =========================
            // PHYSICAL FILE PATH
            // =========================

            var physicalFilePath =
                Path.Combine(
                    uploadFolder,
                    storedFileName);


            // =========================
            // SAVE FILE
            // =========================

            using (var stream =
                new FileStream(
                    physicalFilePath,
                    FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            // =========================
            // DATABASE FILE PATH
            // =========================

            var databaseFilePath =
                $"/Uploads/LoanDocuments/{storedFileName}";


            // =========================
            // SAVE DATABASE RECORD
            // =========================

            var result =
                await _service.CreateAsync(
                    model,
                    file.FileName,
                    storedFileName,
                    databaseFilePath,
                    file.ContentType,
                    file.Length);


            // =========================
            // RESPONSE
            // =========================

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = result.LoanDocumentId
                },
                result);
        }


        // =========================
        // UPDATE / VERIFY
        // =========================

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult>
            Update(
                int id,
                UpdateLoanDocumentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _service.UpdateAsync(
                    id,
                    model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Loan document not found."
                });
            }

            return Ok(result);
        }
        // =========================
        // DOWNLOAD DOCUMENT
        // GET: api/LoanDocument/download/{id}
        // =========================
        // =========================
        // DOWNLOAD DOCUMENT
        // GET: api/LoanDocument/download/{id}
        // =========================

        [HttpGet("download/{id:int}")]
        [Authorize(Roles = "Admin,Customer,LoanOfficer")]
        public async Task<IActionResult> Download(int id)
        {
            // Database se document nikalo
            var document =
                await _service.GetByIdAsync(id);

            if (document == null)
            {
                return NotFound(new
                {
                    message = "Loan document not found."
                });
            }

            // wwwroot folder ka path
            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath =
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot");
            }

            // Database mein stored path:
            // /Uploads/LoanDocuments/xxxxx.pdf
            //
            // Starting "/" remove karke
            // physical path banayenge.

            var relativePath =
                document.FilePath
                    .TrimStart('/')
                    .Replace(
                        '/',
                        Path.DirectorySeparatorChar);

            var physicalFilePath =
                Path.Combine(
                    webRootPath,
                    relativePath);

            // Physical file check
            if (!System.IO.File.Exists(physicalFilePath))
            {
                return NotFound(new
                {
                    message = "Physical file not found.",
                    databasePath = document.FilePath,
                    physicalPath = physicalFilePath
                });
            }

            // File read karo
            var fileBytes =
                await System.IO.File.ReadAllBytesAsync(
                    physicalFilePath);

            // Download return karo
            return File(
                fileBytes,
                document.ContentType ??
                    "application/octet-stream",
                document.DocumentName);
        }


        // =========================
        // DELETE
        // =========================

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Loan document not found."
                });
            }

            return Ok(new
            {
                message =
                    "Loan document deleted successfully."
            });
        }
    }
}