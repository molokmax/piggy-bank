using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persist;
using Persist.Model;

namespace Handler.Movement
{
    public class CreateMovementHandler : IHandler<Api.Model.Movement, Api.Model.Movement>
    {
        private readonly DatabaseContext _context;

        public CreateMovementHandler(DatabaseContext context)
        {
            _context = context;
        }

        public Api.Model.Movement Execute(Api.Model.Movement request)
        {
            StoreItemPersistModel sourceStoreItem = GetStoreItem(request.SourcePurposeId, request.SourceStorageId);
            StoreItemPersistModel destStoreItem = GetStoreItem(request.DestinationPurposeId, request.DestinationStorageId);

            if (sourceStoreItem != null)
            {
                sourceStoreItem.Quantity -= request.Quantity;
            }
            if (destStoreItem != null)
            {
                destStoreItem.Quantity += request.Quantity;
            }

            var history = new MovementPersistModel()
            {
                SourceStoreItem = sourceStoreItem,
                DestinationStoreItem = destStoreItem,
                Quantity = request.Quantity,
                MoveDate = DateTime.Now,
                Comment = request.Comment
            };
            _context.Movements.Add(history);

            _context.SaveChanges();
            request.Id = history.Id;
            return request;
        }

        private StoreItemPersistModel GetStoreItem(long? purposeId, long? storageId)
        {
            StoreItemPersistModel storeItem = null;
            if (purposeId.HasValue && storageId.HasValue)
            {
                storeItem = _context.StoreItems
                    .FirstOrDefault(x => !x.Disabled && x.PurposeId == purposeId.Value && x.StorageId == storageId.Value);
                if (storeItem == null)
                {
                    storeItem = new StoreItemPersistModel()
                    {
                        PurposeId = purposeId.Value,
                        StorageId = storageId.Value
                    };
                    _context.StoreItems.Add(storeItem);
                }
            }
            return storeItem;
        }
    }
}
