using System;
using Api.Model;
using Handler.Purpose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiStartUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurposeController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public PurposeController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public ReadResponse<Purpose> Get(StdReadRequest request)
        {
            ReadPurposeHandler handler = _serviceProvider.GetService<ReadPurposeHandler>();
            ReadResponse<Purpose> response = handler.Execute(request);
            return response;
        }

        [HttpPost]
        public Purpose Post(Purpose request)
        {
            CreatePurposeHandler handler = _serviceProvider.GetService<CreatePurposeHandler>();
            Purpose response = handler.Execute(request);
            return response;
        }

        [HttpPut]
        public Purpose Put(Purpose request)
        {
            UpdatePurposeHandler handler = _serviceProvider.GetService<UpdatePurposeHandler>();
            Purpose response = handler.Execute(request);
            return response;
        }

        [HttpDelete]
        public Purpose Delete(Purpose request)
        {
            DeletePurposeHandler handler = _serviceProvider.GetService<DeletePurposeHandler>();
            Purpose response = handler.Execute(request);
            return response;
        }
    }
}
