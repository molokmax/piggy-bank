using Persist;
using Persist.Model;

namespace Handler.Storage
{
    public class UpdateStorageHandler : IHandler<Api.Model.Storage, Api.Model.Storage>
    {
        private readonly DatabaseContext _context;

        public UpdateStorageHandler(DatabaseContext context)
        {
            _context = context;
        }

        public Api.Model.Storage Execute(Api.Model.Storage request)
        {
            StoragePersistModel record = _context.Storages.Find(request.Id);
            record.Name = request.Name;
            _context.SaveChanges();
            request.Id = record.Id;
            return request;
        }
    }
}
