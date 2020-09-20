using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly ICsgobackpackService _csgobackpackService;

        public BitskinsController(
            IMapper mapper,
            ILogger<BitskinsController> logger,
            IBitskinsService bitskinsService,
            ICsgobackpackService csgobackpackService)
        {
            _mapper = mapper;
            _logger = logger;
            _bitskinsService = bitskinsService;
            _csgobackpackService = csgobackpackService;
        }

        // [Authorize]
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

        // [Authorize(Role.System)]
        [HttpPost("process-items")]
        public ActionResult Create(ProcessItemsRequest model)
        {
            return Ok(model);
        }

        [HttpGet("item/buy-test")]
        public async Task<ActionResult> BuyTest()
        {
            List<BitskinsItem> itemsList = new List<BitskinsItem>();
            BitskinsItem item1 = new BitskinsItem();
            BitskinsItem item2 = new BitskinsItem();
            BitskinsItem item3 = new BitskinsItem();

            item2.ItemId = "14108606168";
            item2.MarketHashName = "Sealed Graffiti | X-Knives (Desert Amber)";
            item2.Price = 0.01M;
            itemsList.Add(item2);

            item1.ItemId = "14108607050";
            item1.MarketHashName = "Sealed Graffiti | X-Axes (Brick Red)";
            item1.Price = 0.01M;
            itemsList.Add(item1);

            item3.ItemId = "14108607050";
            item3.MarketHashName = "TEST TEST";
            item3.Price = 0.01M;
            itemsList.Add(item3);

            await _bitskinsService.ProcessItems(itemsList);

            return Ok("done");
        }

        // [Authorize]
        [HttpGet("item/{itemName}/external-price")]
        public async Task<ActionResult> GetItemPrice(string itemName)
        {
            ItemPrice itemPrice = await _csgobackpackService.GetItemPrice(itemName);
            return Ok(itemPrice);
        }
    }
}
