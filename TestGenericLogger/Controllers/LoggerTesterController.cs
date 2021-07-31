using Microsoft.AspNetCore.Mvc;

namespace TestGenericLogger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggerTesterController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            GenericLogger.Logger.WriteDebug("Test debug message");
            GenericLogger.Logger.WriteInformation("Test information message");
            GenericLogger.Logger.WriteWarning("Test warning message");
            GenericLogger.Logger.WriteError("Test error message");

            return "Logging worked ok";
        }
    }
}
