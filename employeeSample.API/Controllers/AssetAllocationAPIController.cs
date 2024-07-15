using employeeSample.API.DTO;
using employeeSample.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace employeeSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetAllocationAPIController : ControllerBase
    {
        private readonly AssetManagementContext _dbContext;

        public AssetAllocationAPIController(AssetManagementContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Allocations")]
        public async Task<IActionResult> GetAllocations()
        {
            try
            {
                var allocations = await _dbContext.AssetAllocations
                    .Select(item => new AssetAllocationDto
                    {
                        AllocationId = item.AllocationId,
                        EmployeeId = item.EmployeeId,
                        AssetId = item.AssetId,
                        isActive = item.isActive,
                        AllocatedOn = item.AllocatedOn,
                        AllocatedBy = item.AllocatedBy
                    })
                    .ToListAsync();

                if (allocations.Count > 0)
                {
                    return Ok(allocations);
                }
                else
                {
                    return Ok("No Allocations in the database");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("Allocations/{id:int}")]
        public async Task<IActionResult> GetAllocationsById(int id)
        {
            try
            {
                var allocation = await _dbContext.AssetAllocations
                    .Where(e => e.AllocationId == id)
                    .Select(item => new AssetAllocationDto
                    {
                        AllocationId = item.AllocationId,
                        EmployeeId = item.EmployeeId,
                        AssetId = item.AssetId,
                        isActive = item.isActive,
                        AllocatedOn = item.AllocatedOn,
                        AllocatedBy = item.AllocatedBy
                    })
                    .FirstOrDefaultAsync();

                if (allocation != null)
                {
                    return Ok(allocation);
                }
                else
                {
                    return NotFound("Allocation not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet]
        [Route("AllocationsByEmpId/{id:int}")]
        public async Task<IActionResult> GetAllocationsByEmpId(int id)
        {
            try
            {
                var allocations = await _dbContext.AssetAllocations
                    .Where(e => e.EmployeeId == id)
                    .Select(item => new AssetPropertiesDto
                    {
                        AllocationId = item.AllocationId,
                        EmployeeId = item.EmployeeId,
                        AssetId = item.AssetId,
                        SerialNumber = item.Asset.SerialNumber,
                        InventoryId = item.Asset.InventoryId,
                        AssetType = item.Asset.Inventory.AssetType,
                        Brand = item.Asset.Inventory.Brand,
                        Model = item.Asset.Inventory.Model
                    })
                    .ToListAsync(); // Fetch all allocations

                if (allocations.Any())
                {
                    return Ok(allocations);
                }
                else
                {
                    return NotFound("No allocations found for the given employee ID");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }




        [HttpPost]
        [Route("Allocations")]
        public async Task<IActionResult> CreateAllocation([FromBody] AssetAllocationDto allocationdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newAllocation = new AssetAllocation
                {
                    AllocationId = allocationdto.AllocationId,
                    EmployeeId = allocationdto.EmployeeId,
                    AssetId = allocationdto.AssetId,
                    isActive = allocationdto.isActive,
                    AllocatedBy = allocationdto.AllocatedBy,
                    AllocatedOn = DateTime.UtcNow,
                };

                _dbContext.AssetAllocations.Add(newAllocation);
                await _dbContext.SaveChangesAsync();

                return Ok(newAllocation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("Allocations/{id:int}")]
        public async Task<IActionResult> DeleteAllocation(int id)
        {
            try
            {
                var allocation = await _dbContext.AssetAllocations.FindAsync(id);
                if (allocation == null)
                {
                    return NotFound("Allocation not found");
                }

                _dbContext.AssetAllocations.Remove(allocation);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Allocations/{id:int}")]
        public async Task<IActionResult> UpdateAllocation(int id, [FromBody] AssetAllocationDto allocationdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var allocation = await _dbContext.AssetAllocations.FindAsync(id);
                if (allocation == null)
                {
                    return NotFound("Allocation not found");
                }

                allocation.EmployeeId = allocationdto.EmployeeId;
                    allocation.AssetId = allocationdto.AssetId;
                    allocation.isActive = allocationdto.isActive;
                    allocation.AllocatedBy = allocationdto.AllocatedBy;

                _dbContext.AssetAllocations.Update(allocation);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
