using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Bitskins;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BitskinsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBitskinsService _bitskinsService;

        public BitskinsController(
            IMapper mapper,
            ILogger<BitskinsController> logger,
            IBitskinsService bitskinsService)
        {
            _mapper = mapper;
            _logger = logger;
            _bitskinsService = bitskinsService;
        }

        [Authorize]
        [HttpGet("balance")]
        public async Task<ActionResult> GetAccountBalance()
        {
            var balance = await _bitskinsService.GetAccountBalance();
            return Ok(balance);
        }

        [Authorize]
        [HttpGet("buy-history")]
        public async Task<ActionResult> GetBuyHistory()
        {
            var history = await _bitskinsService.GetBuyHistory();
            return Ok(history);
        }

        [Authorize]
        [HttpGet("recent-sales/{itemName}")]
        public async Task<ActionResult> GetRecentSalesInfo(string itemName)
        {
            var itemSales = await _bitskinsService.GetRecentSalesInfo(itemName);
            return Ok(itemSales);
        }

        [Authorize]
        [HttpPost("process-items")]
        public async Task<ActionResult> ProcessItems(ProcessItemsRequest model)
        {
            await _bitskinsService.ProcessItems(model.Items, Account.Id);
            return Ok();
        }
    }
}
