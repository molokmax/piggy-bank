using System;
using System.Collections.Generic;
using System.Text;
using Persist;
using Persist.Model;

namespace Handler.Purpose
{
    public class CreatePurposeHandler : IHandler<Api.Model.Purpose, Api.Model.Purpose>
    {
        private readonly DatabaseContext _context;

        public CreatePurposeHandler(DatabaseContext context)
        {
            _context = context;
        }

        public Api.Model.Purpose Execute(Api.Model.Purpose request)
        {
            var record = new PurposePersistModel()
            {
                Name = request.Name
            };
            _context.Purposes.Add(record);
            _context.SaveChanges();
            request.Id = record.Id;
            return request;
        }
    }
}
