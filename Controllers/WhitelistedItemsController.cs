﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.WhitelistedItems;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhitelistedItemsController : BaseController
    {
        private readonly IWhitelistedItemService _whitelistedItemService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBitskinsService _bitskinsService;

        public WhitelistedItemsController(
            IWhitelistedItemService whitelistedItemService,
            IMapper mapper,
            ILogger<WhitelistedItemsController> logger,
            IBitskinsService bitskinsService)
        {
            _whitelistedItemService = whitelistedItemService;
            _mapper = mapper;
            _logger = logger;
            _bitskinsService = bitskinsService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<WhitelistedItemResponse>> GetAll()
        {
            var items = _whitelistedItemService.GetAll();
            return Ok(items);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public ActionResult<WhitelistedItemResponse> GetById(int id)
        {
            var item = _whitelistedItemService.GetById(id);

            // users can get their own items and admins can get any item
            if (item.AccountId != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            return Ok(item);
        }

        [Authorize]
        [HttpGet("get-by-name/{name}")]
        public ActionResult<WhitelistedItemResponse> GetByName(string name)
        {
            var item = _whitelistedItemService.GetByNameAndAccountId(name, Account.Id);
            return Ok(item);
        }

        [Authorize]
        [HttpGet("outdated-prices/{amount:int}")]
        public ActionResult<WhitelistedItemResponse> GetItemsWithOutdatedPrices(int amount)
        {
            var items = _whitelistedItemService.GetItemsWithOutdatedPrices(amount);
            return Ok(items);
        }

        [Authorize]
        [HttpPost("update-prices")]
        public ActionResult<WhitelistedItemResponse> UpdatePrices(UpdatePricesRequest model)
        {
            _whitelistedItemService.UpdatePrices(model);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public ActionResult<WhitelistedItemResponse> Create(CreateRequest model)
        {
            model.AccountId = Account.Id;

            var item = _whitelistedItemService.Create(model);
            return Ok(item);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public ActionResult<WhitelistedItemResponse> Update(int id, UpdateRequest model)
        {
            var item = _whitelistedItemService.GetById(id);

            // users can update their own items and admins can update any item
            if (item.AccountId != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            model.AccountId = Account.Id;

            var updatedItem = _whitelistedItemService.Update(id, model);
            return Ok(updatedItem);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = _whitelistedItemService.GetById(id);

            // users can delete their own items and admins can delete any item
            if (item.AccountId != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _whitelistedItemService.Delete(id);
            return Ok(new { message = "Item deleted successfully" });
        }
    }
}
