using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Api.Model;
using Persist;
using Persist.Model;

namespace Handler.Purpose
{
    public class ReadPurposeHandler : IHandler<StdReadRequest, ReadResponse<Api.Model.Purpose>>
    {
        private readonly DatabaseContext _context;

        public ReadPurposeHandler(DatabaseContext context)
        {
            _context = context;
        }

        public ReadResponse<Api.Model.Purpose> Execute(StdReadRequest request)
        {
            IQueryable<PurposePersistModel> records = _context.Purposes.Where(x => !x.Disabled);
            List<Api.Model.Purpose> result = records.Select(x => new Api.Model.Purpose()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            var response = new ReadResponse<Api.Model.Purpose>(result, result.Count);
            return response;
        }
    }
}
