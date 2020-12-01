using System.Collections.Generic;
using System.Linq;
using Api.Model;
using Persist;
using Persist.Model;

namespace Handler.Storage
{
    public class ReadStorageHandler : IHandler<StdReadRequest, ReadResponse<Api.Model.Storage>>
    {
        private readonly DatabaseContext _context;

        public ReadStorageHandler(DatabaseContext context)
        {
            _context = context;
        }

        public ReadResponse<Api.Model.Storage> Execute(StdReadRequest request)
        {
            IQueryable<StoragePersistModel> records = _context.Storages.Where(x => !x.Disabled);
            List<Api.Model.Storage> result = records.Select(x => new Api.Model.Storage()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            var response = new ReadResponse<Api.Model.Storage>(result, result.Count);
            return response;
        }
    }
}
