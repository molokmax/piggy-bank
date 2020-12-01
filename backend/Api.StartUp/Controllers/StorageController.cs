using System;
using Api.Model;
using Handler.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiStartUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public StorageController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public ReadResponse<Storage> Get(StdReadRequest request)
        {
            // StdReadRequest request = new StdReadRequest();
            ReadStorageHandler handler = _serviceProvider.GetService<ReadStorageHandler>();
            ReadResponse<Storage> response = handler.Execute(request);
            return response;
        }

        [HttpPost]
        public Storage Post(Storage request)
        {
            CreateStorageHandler handler = _serviceProvider.GetService<CreateStorageHandler>();
            Storage response = handler.Execute(request);
            return response;
        }

        [HttpPut]
        public Storage Put(Storage request)
        {
            UpdateStorageHandler handler = _serviceProvider.GetService<UpdateStorageHandler>();
            Storage response = handler.Execute(request);
            return response;
        }

        [HttpDelete]
        public Storage Delete(Storage request)
        {
            DeleteStorageHandler handler = _serviceProvider.GetService<DeleteStorageHandler>();
            Storage response = handler.Execute(request);
            return response;
        }
    }
}
