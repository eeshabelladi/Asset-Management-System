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
    public class AssetAPIController : ControllerBase
    {
        private readonly AssetManagementContext _dbContext;

        public AssetAPIController(AssetManagementContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Assets")]
        public async Task<IActionResult> GetAssets()
        {
            try
            {
                var assets = await _dbContext.Assets
                    .Select(item => new AssetDto
                    {
                        AssetId = item.AssetId,
                        SerialNumber = item.SerialNumber,
                        InventoryId = item.InventoryId,
                        WarrantyStartDate = item.WarrantyStartDate,
                        WarrantyEndDate = item.WarrantyEndDate,
                        isAvailable = item.isAvailable,
                        AssetCreatedBy = item.AssetCreatedBy
                    })
                    .ToListAsync();

                if (assets.Count > 0)
                {
                    return Ok(assets);
                }
                else
                {
                    return Ok("No Assets in the database");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("Assets/{id:int}")]
        public async Task<IActionResult> GetAssetBySno(int id)
        {
            try
            {
                var asset = await _dbContext.Assets
                    .Where(e => e.AssetId == id)
                    .Select(item => new AssetDto
                    {

                        AssetId = item.AssetId,
                        SerialNumber = item.SerialNumber,
                        InventoryId = item.InventoryId,
                        WarrantyStartDate = item.WarrantyStartDate,
                        WarrantyEndDate = item.WarrantyEndDate,
                        isAvailable = item.isAvailable,
                        AssetCreatedBy = item.AssetCreatedBy
                    })
                    .FirstOrDefaultAsync();

                if (asset != null)
                {
                    return Ok(asset);
                }
                else
                {
                    return NotFound("Asset not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("Assets")]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto assetdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newAsset = new Asset
                {
                    AssetId = assetdto.AssetId,
                    SerialNumber = assetdto.SerialNumber,
                    InventoryId = assetdto.InventoryId,
                    WarrantyStartDate = assetdto.WarrantyStartDate,
                    WarrantyEndDate = assetdto.WarrantyEndDate,
                    isAvailable = assetdto.isAvailable,
                    AssetCreatedBy = assetdto.AssetCreatedBy,
                    AssetCreatedOn = DateTime.UtcNow,
                };

                _dbContext.Assets.Add(newAsset);
                await _dbContext.SaveChangesAsync();

                return Ok(newAsset);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("Assets/{id:int}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            try
            {
                var asset = await _dbContext.Assets.FindAsync(id);
                if (asset == null)
                {
                    return NotFound("Asset not found");
                }

                _dbContext.Assets.Remove(asset);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Assets/{id:int}")]
        public async Task<IActionResult> UpdateAsset(int id, [FromBody] AssetDto assetdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var asset = await _dbContext.Assets.FindAsync(id);
                if (asset == null)
                {
                    return NotFound("Asset not found");
                }
                asset.SerialNumber = assetdto.SerialNumber;
                asset.InventoryId = assetdto.InventoryId;
                asset.WarrantyStartDate = assetdto.WarrantyStartDate;
                asset.WarrantyEndDate = assetdto.WarrantyEndDate;
                asset.isAvailable = assetdto.isAvailable;
                asset.AssetCreatedBy = assetdto.AssetCreatedBy;

                _dbContext.Assets.Update(asset);
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
