using employeeSample.API.Models;
using employeeSample.API.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using employeeSample.API.DTO;
using System.Security.Claims;

namespace employeeSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestAPIController : ControllerBase
    {
        private readonly AssetManagementContext _dbContext;

        public RequestAPIController(AssetManagementContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Requests")]
        public async Task<IActionResult> GetRequests()
        {
            try
            {
                var requests = await _dbContext.Requests
        .Select(item => new RequestDto
        {
            RequestId = item.RequestId,
            ReqCreatedBy = item.ReqCreatedBy,
            AssetId = item.AssetId,
            ReqCreatedOn = item.ReqCreatedOn,
            ReqStatus = item.ReqStatus,
            ApprovedOn = item.ApprovedOn,
            RequestType = item.RequestType,
            ReqAssetType = item.ReqAssetType,
            ApprovedBy = item.ApprovedBy,
            Reason = item.Reason
        })
        .ToListAsync();

                if (requests.Count > 0)
                {
                    return Ok(requests);
                }
                else
                {
                    return Ok("No requests in the database");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        [Route("Requests")]
        public async Task<IActionResult> CreateRequest([FromBody] RequestDto reqdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newRequest = new Request
                {
                    RequestId = reqdto.RequestId,
                    ReqCreatedBy = reqdto.ReqCreatedBy,
                    AssetId = reqdto.AssetId,
                    ReqStatus = reqdto.ReqStatus,
                    ApprovedOn = reqdto.ApprovedOn,
                    RequestType = reqdto.RequestType,
                    ReqAssetType=reqdto.ReqAssetType,
                    ApprovedBy = reqdto.ApprovedBy,
                    Reason = reqdto.Reason,
                    ReqCreatedOn = DateTime.UtcNow,
                };

                _dbContext.Requests.Add(newRequest);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
