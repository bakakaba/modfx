using Microsoft.Extensions.Logging;

namespace DotNetContainer.Testing.Tests.Resources
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
            return _testInterface.GetString();
        }
    }
}