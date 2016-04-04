namespace DNC.Testing.Tests.Resources
{
    public class GetStringService
    {
        IMockableInterface _testInterface;
        public GetStringService(IMockableInterface testInterface)
        {
            _testInterface = testInterface;
        }
        
        public string GetString() {
            return _testInterface.GetString();
        }
    }
}