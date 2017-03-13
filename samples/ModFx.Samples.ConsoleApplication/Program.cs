using ModFx.Logging.File;
using ModFx.Samples.ConsoleApplication.Services;

namespace ModFx.Samples.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var fx = Framework.Initialize(typeof(FileLogging)))
            {
                var console = fx.Resolve<IColoredConsole>();
                console.WriteLine("&#cyan#>Hello &#green#>world!!");
            }
        }
    }
}