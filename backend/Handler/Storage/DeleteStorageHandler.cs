using System;
using Persist;
using Persist.Model;

namespace Handler.Storage
{
    public class DeleteStorageHandler : IHandler<Api.Model.Storage, Api.Model.Storage>
    {
        private readonly DatabaseContext _context;

        public DeleteStorageHandler(DatabaseContext context)
        {
            _context = context;
        }

        public Api.Model.Storage Execute(Api.Model.Storage request)
        {
            StoragePersistModel record = _context.Storages.Find(request.Id);
            record.Disabled = true;
            record.DisableDate = DateTime.Now;
            _context.SaveChanges();
            request.Id = record.Id;
            return request;
        }
    }
}
