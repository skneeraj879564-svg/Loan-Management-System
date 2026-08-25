using Loan_Management_System_Business.Dtos.Branch;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // ==========================================
        // CREATE BRANCH
        // POST: api/Branch
        // ==========================================
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            CreateBranchDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _branchService.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { branchId = result.BranchId },
                result);
        }

        // ==========================================
        // GET ALL BRANCHES
        // GET: api/Branch
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _branchService.GetAllAsync();

            return Ok(result);
        }

        // ==========================================
        // GET BRANCH BY ID
        // GET: api/Branch/1
        // ==========================================
        [HttpGet("{branchId}")]
        public async Task<IActionResult> GetById(
            int branchId)
        {
            var result =
                await _branchService.GetByIdAsync(branchId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Branch not found."
                });
            }

            return Ok(result);
        }

        // ==========================================
        // UPDATE BRANCH
        // PUT: api/Branch/1
        // ==========================================
        [HttpPut("{branchId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int branchId,
            UpdateBranchDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _branchService.UpdateAsync(
                    branchId,
                    model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Branch not found."
                });
            }

            return Ok(result);
        }

        // ==========================================
        // DELETE BRANCH
        // DELETE: api/Branch/1
        // ==========================================
        [HttpDelete("{branchId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int branchId)
        {
            var result =
                await _branchService.DeleteAsync(branchId);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Branch not found."
                });
            }

            return Ok(new
            {
                message = "Branch deleted successfully."
            });
        }
    }
}
