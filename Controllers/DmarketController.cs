using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DmarketController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDmarketService _dmarketService;

        public DmarketController(
            IMapper mapper,
            ILogger<DmarketController> logger,
            IDmarketService dmarketService)
        {
            _mapper = mapper;
            _logger = logger;
            _dmarketService = dmarketService;
        }

        [Authorize]
        [HttpGet("account/balance")]
        public async Task<ActionResult> GetAccountBalance()
        {
            var balance = await _dmarketService.GetAccountBalance();
            return Ok(balance);
        }

        // [Authorize]
        [HttpGet("targets")]
        public async Task<ActionResult> GetTargets()
        {
            var targets = await _dmarketService.GetTargets();
            return Ok(targets);
        }

        // [Authorize]
        [HttpGet("sale-offers")]
        public async Task<ActionResult> GetSaleOffers()
        {
            var offers = await _dmarketService.GetSaleOffers();
            return Ok(offers);
        }
    }
}
