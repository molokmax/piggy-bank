using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Persist;
using Persist.Model;

namespace ApiStartUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public TestController(ILogger<TestController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public string Get()
        {
            DatabaseContext db = _serviceProvider.GetService<DatabaseContext>();
            var storage = new StoragePersistModel()
            {
                Name = "test storage"
            };
            db.Storages.Add(storage);
            db.SaveChanges();
            return "OK";
        }
    }
}
