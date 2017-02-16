namespace DotNetContainer.Samples.ConsoleApplication.Services
{
    public interface IColoredConsole
    {
        void Write(string text);
        void WriteLine(string text);
    }
}