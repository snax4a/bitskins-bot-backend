using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.WhitelistedItems;
using Microsoft.Extensions.Logging;

namespace WebApi.Services
{
    public interface IWhitelistedItemService
    {
        IEnumerable<WhitelistedItemResponse> GetAll();
        WhitelistedItemResponse GetById(int id);
        WhitelistedItemResponse GetByName(string name);
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

        public WhitelistedItemResponse GetByName(string name)
        {
            var item = _context.WhitelistedItems.FirstOrDefault(i => i.Name == name);
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
            var item = getWhitelistedItem(id);

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
