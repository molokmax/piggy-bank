using System;
using Api.Model;
using Handler.MovementHistory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiStartUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementHistoryController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public MovementHistoryController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public ReadResponse<MovementHistory> Get(StdReadRequest request)
        {
            ReadMovementHistoryHandler handler = _serviceProvider.GetService<ReadMovementHistoryHandler>();
            ReadResponse<MovementHistory> response = handler.Execute(request);
            return response;
        }
    }
}
