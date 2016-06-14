using Microsoft.Extensions.Logging;

namespace DNC.Testing.Tests.Resources
{
    public class GetStringService
    {
        private ILogger _logger;
        private IMockableInterface _testInterface;

        public GetStringService(ILogger<GetStringService> logger, IMockableInterface testInterface)
        {
            _logger = logger;
            _testInterface = testInterface;
        }

        public string GetString() {
            _logger.LogInformation("Get string called.");
            return _testInterface.GetString();
        }
    }
}