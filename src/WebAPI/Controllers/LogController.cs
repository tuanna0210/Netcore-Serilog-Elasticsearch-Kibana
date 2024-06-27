using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "LogInformation")]
        public IActionResult LogInformation([FromBody] Input input)
        {
            _logger.LogInformation("Body input {@body}", input);

            return Ok();
        }

        [HttpPost("/information-with-log-context")]
        public IActionResult LogInformationWithLogContext([FromBody] Input input)
        {
            //using (LogContext.PushProperty("CorrelationId", Guid.NewGuid()))
            //using (LogContext.PushProperty("Method", "POST"))
            //{
            //    _logger.LogInformation("Body {@body}", input);
            //}

            using (LogContext.PushProperty("CorrelationId", Guid.NewGuid()))
            using (LogContext.PushProperty("Method", "POST"))
                _logger.LogInformation("Body {@body}", input);

            return Ok();
        }
    }

    public class Input
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
