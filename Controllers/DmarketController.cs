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

        // [Authorize]
        [HttpGet("account/balance")]
        public async Task<ActionResult> GetAccountBalance()
        {
            var balance = await _dmarketService.GetAccountBalance();
            return Ok(balance);
        }
    }
}
