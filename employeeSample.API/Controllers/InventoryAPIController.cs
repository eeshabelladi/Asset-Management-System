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
    public class InventoryAPIController : ControllerBase
    {
        private readonly AssetManagementContext _dbContext;

        public InventoryAPIController(AssetManagementContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Inventories")]
        public async Task<IActionResult> GetInventories()
        {
            try
            {
                var inventories = await _dbContext.Inventories
                    .Select(item => new InventoryDto
                    {
                       InventoryId = item.InventoryId,
                       Brand = item.Brand,
                       Model = item.Model,
                       Quantity = item.Quantity,
                       AssetType = item.AssetType,
                       InvCreatedBy = item.InvCreatedBy
                    })
                    .ToListAsync();

                if (inventories.Count > 0)
                {
                    return Ok(inventories);
                }
                else
                {
                    return Ok("No inventories in the database");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet]
        [Route("Inventories/{id:int}")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            try
            {
                var inventory = await _dbContext.Inventories
                    .Where(e => e.InventoryId == id)
                    .Select(item => new InventoryDto
                    {
                        InventoryId = item.InventoryId,
                        Brand = item.Brand,
                        Model = item.Model,
                        Quantity = item.Quantity,
                        AssetType = item.AssetType,
                        InvCreatedBy = item.InvCreatedBy
                    })
                    .FirstOrDefaultAsync();

                if (inventory != null)
                {
                    return Ok(inventory);
                }
                else
                {
                    return NotFound("Inventory not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("Inventories")]
        public async Task<IActionResult> CreateInventory([FromBody] InventoryDto inventorydto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newInventory = new Inventory
                {
                    InventoryId = inventorydto.InventoryId,
                    Brand = inventorydto.Brand,
                    Model = inventorydto.Model,
                    Quantity = inventorydto.Quantity,
                    AssetType = inventorydto.AssetType,
                    InvCreatedBy = inventorydto.InvCreatedBy,
                    InvCreatedOn = DateTime.UtcNow,
                };

                _dbContext.Inventories.Add(newInventory);
                await _dbContext.SaveChangesAsync();

                return Ok(newInventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("Inventories/{id:int}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                var inventory = await _dbContext.Inventories.FindAsync(id);
                if (inventory == null)
                {
                    return NotFound("Inventory not found");
                }

                _dbContext.Inventories.Remove(inventory);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Inventories/{id:int}")]
        public async Task<IActionResult> Updateinventory(int id, [FromBody] InventoryDto inventorydto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var inventory = await _dbContext.Inventories.FindAsync(id);
                if (inventory == null)
                {
                    return NotFound("Inventory not found");
                }

                inventory.Brand = inventorydto.Brand;
                    inventory.Model = inventorydto.Model;
                    inventory.Quantity = inventorydto.Quantity;
                    inventory.AssetType = inventorydto.AssetType;
                    inventory.InvCreatedBy = inventorydto.InvCreatedBy;

                _dbContext.Inventories.Update(inventory);
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
