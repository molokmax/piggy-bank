using Persist;
using Persist.Model;

namespace Handler.Storage
{
    public class CreateStorageHandler : IHandler<Api.Model.Storage, Api.Model.Storage>
    {
        private readonly DatabaseContext _context;

        public CreateStorageHandler(DatabaseContext context)
        {
            _context = context;
        }

        public Api.Model.Storage Execute(Api.Model.Storage request)
        {
            var record = new StoragePersistModel()
            {
                Name = request.Name
            };
            _context.Storages.Add(record);
            _context.SaveChanges();
            request.Id = record.Id;
            return request;
        }
    }
}
