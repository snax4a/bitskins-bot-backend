using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.WhitelistedItems;
using Microsoft.Extensions.Logging;
using System;

namespace WebApi.Services
{
    public interface IWhitelistedItemService
    {
        IEnumerable<WhitelistedItemResponse> GetAll();
        WhitelistedItemResponse GetById(int id);
        WhitelistedItemResponse GetByNameAndAccountId(string name, int accountId);
        IEnumerable<OutdatedItemResponse> GetItemsWithOutdatedPrices(int amount);
        void UpdatePrices(UpdatePricesRequest model);
        WhitelistedItemResponse Create(CreateRequest model);
        WhitelistedItemResponse Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class WhitelistedItemService : IWhitelistedItemService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<WhitelistedItemService> _logger;

        public WhitelistedItemService(
            DataContext context,
            IMapper mapper,
            ILogger<WhitelistedItemService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<WhitelistedItemResponse> GetAll()
        {
            var items = _context.WhitelistedItems;
            return _mapper.Map<IList<WhitelistedItemResponse>>(items);
        }

        public WhitelistedItemResponse GetById(int id)
        {
            var item = getWhitelistedItem(id);
            return _mapper.Map<WhitelistedItemResponse>(item);
        }

        public IEnumerable<OutdatedItemResponse> GetItemsWithOutdatedPrices(int amount)
        {
            var lastUpdate = DateTime.UtcNow.AddHours(-6);
            var items = _context.WhitelistedItems
                .Where(i => i.PriceUpdatedAt <= lastUpdate)
                .OrderBy(i => i.PriceUpdatedAt)
                .Select(i => new OutdatedItemResponse { Id = i.Id, Name = i.Name, PriceUpdatedAt = i.PriceUpdatedAt })
                .Take(amount);

            return items;
        }

        public void UpdatePrices(UpdatePricesRequest model)
        {
            foreach (var data in model.PriceData)
            {
                var item = getWhitelistedItem(data.Id);

                // copy model to item and save
                _mapper.Map(data, item);
                item.PriceUpdatedAt = DateTime.UtcNow;
                _context.WhitelistedItems.Update(item);
                _context.SaveChanges();
            }
        }

        public WhitelistedItemResponse GetByNameAndAccountId(string name, int accountId)
        {
            var item = _context.WhitelistedItems.FirstOrDefault(i => i.Name == name && i.AccountId == accountId);
            return _mapper.Map<WhitelistedItemResponse>(item);
        }

        public WhitelistedItemResponse Create(CreateRequest model)
        {
            // validate
            if (_context.WhitelistedItems.Any(x => x.Name == model.Name && x.AccountId == model.AccountId))
                throw new AppException($"Item '{model.Name}' is already whitelisted");

            // map model to new account object
            var item = _mapper.Map<WhitelistedItem>(model);

            // save item
            _context.WhitelistedItems.Add(item);
            _context.SaveChanges();

            return _mapper.Map<WhitelistedItemResponse>(item);
        }

        public WhitelistedItemResponse Update(int id, UpdateRequest model)
        {
            // validate
            if (_context.WhitelistedItems.Any(i => i.Name == model.Name && i.AccountId == model.AccountId && i.Id != id))
                throw new AppException($"Item '{model.Name}' is already whitelisted");

            var item = getWhitelistedItem(id);

            // if item name was updated we need to reset its price
            if (model.Name != item.Name)
            {
                item.Price = 0;
                item.PriceUpdatedAt = DateTime.UtcNow.AddHours(-24);
            }

            // copy model to item and save
            _mapper.Map(model, item);
            _context.WhitelistedItems.Update(item);
            _context.SaveChanges();

            return _mapper.Map<WhitelistedItemResponse>(item);
        }

        public void Delete(int id)
        {
            var item = getWhitelistedItem(id);
            _context.WhitelistedItems.Remove(item);
            _context.SaveChanges();
        }

        // helper methods

        private WhitelistedItem getWhitelistedItem(int id)
        {
            var item = _context.WhitelistedItems.Find(id);
            if (item == null) throw new KeyNotFoundException("Item not found");
            return item;
        }
    }
}
