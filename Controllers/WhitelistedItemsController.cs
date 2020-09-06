using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        public WhitelistedItemsController(
            IWhitelistedItemService whitelistedItemService,
            IMapper mapper,
            ILogger<WhitelistedItemsController> logger)
        {
            _whitelistedItemService = whitelistedItemService;
            _mapper = mapper;
            _logger = logger;
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
