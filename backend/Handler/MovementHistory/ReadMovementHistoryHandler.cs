using System.Collections.Generic;
using System.Linq;
using Api.Model;
using Persist;
using Persist.Model;

namespace Handler.MovementHistory
{
    public class ReadMovementHistoryHandler : IHandler<StdReadRequest, ReadResponse<Api.Model.MovementHistory>>
    {
        private readonly DatabaseContext _context;

        public ReadMovementHistoryHandler(DatabaseContext context)
        {
            _context = context;
        }

        public ReadResponse<Api.Model.MovementHistory> Execute(StdReadRequest request)
        {
            IQueryable<MovementPersistModel> records = _context.Movements;
            List<Api.Model.MovementHistory> result = records.Select(x => new Api.Model.MovementHistory()
            {
                Id = x.Id,
                SourceStoreItemId = x.SourceStoreItemId,
                SourceStorageId = x.SourceStoreItem.StorageId,
                SourceStorageName = x.SourceStoreItem.Storage.Name,
                SourcePurposeId = x.SourceStoreItem.PurposeId,
                SourcePurposeName = x.SourceStoreItem.Purpose.Name,
                DestinationStoreItemId = x.DestinationStoreItemId,
                DestinationStorageId = x.DestinationStoreItem.StorageId,
                DestinationStorageName = x.DestinationStoreItem.Storage.Name,
                DestinationPurposeId = x.DestinationStoreItem.PurposeId,
                DestinationPurposeName = x.DestinationStoreItem.Purpose.Name,
                Quantity = x.Quantity,
                Comment = x.Comment,
                MoveDate = x.MoveDate
            }).ToList();
            var response = new ReadResponse<Api.Model.MovementHistory>(result, result.Count);
            return response;
        }
    }
}
