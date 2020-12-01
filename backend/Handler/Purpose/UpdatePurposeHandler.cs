using System;
using System.Collections.Generic;
using System.Text;
using Persist;
using Persist.Model;

namespace Handler.Purpose
{
    public class UpdatePurposeHandler : IHandler<Api.Model.Purpose, Api.Model.Purpose>
    {
        private readonly DatabaseContext _context;

        public UpdatePurposeHandler(DatabaseContext context)
        {
            _context = context;
        }

        public Api.Model.Purpose Execute(Api.Model.Purpose request)
        {
            PurposePersistModel record = _context.Purposes.Find(request.Id);
            record.Name = request.Name;
            _context.SaveChanges();
            request.Id = record.Id;
            return request;
        }
    }
}
