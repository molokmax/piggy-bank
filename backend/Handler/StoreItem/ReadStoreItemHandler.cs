using System.Collections.Generic;
using System.Linq;
using Api.Model;
using Persist;
using Persist.Model;

namespace Handler.StoreItem
{
    public class ReadStoreItemHandler : IHandler<StdReadRequest, ReadResponse<Api.Model.StoreItem>>
    {
        private readonly DatabaseContext _context;

        public ReadStoreItemHandler(DatabaseContext context)
        {
            _context = context;
        }

        public ReadResponse<Api.Model.StoreItem> Execute(StdReadRequest request)
        {
            IQueryable<StoreItemPersistModel> records = _context.StoreItems.Where(x => !x.Disabled);
            List<Api.Model.StoreItem> result = records.Select(x => new Api.Model.StoreItem()
            {
                Id = x.Id,
                PurposeId = x.PurposeId,
                StorageId = x.StorageId,
                Quantity = x.Quantity
            }).ToList();
            var response = new ReadResponse<Api.Model.StoreItem>(result, result.Count);
            return response;
        }
    }
}
