using System;
using Api.Model;
using Handler.StoreItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiStartUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreItemController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public StoreItemController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public ReadResponse<StoreItem> Get(StdReadRequest request)
        {
            ReadStoreItemHandler handler = _serviceProvider.GetService<ReadStoreItemHandler>();
            ReadResponse<StoreItem> response = handler.Execute(request);
            return response;
        }
    }
}
