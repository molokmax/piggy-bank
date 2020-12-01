using System;
using Api.Model;
using Handler.Movement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiStartUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public MovementController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        public Movement Post(Movement request)
        {
            CreateMovementHandler handler = _serviceProvider.GetService<CreateMovementHandler>();
            Movement response = handler.Execute(request);
            return response;
        }
    }
}
