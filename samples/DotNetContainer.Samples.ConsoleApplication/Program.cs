using System.Diagnostics;
using DotNetContainer.Samples.ConsoleApplication.Services;

namespace DotNetContainer.Samples.ConsoleApplication
{
    class Program
    {
        static Framework _fx;

        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            _fx = Framework.Initialize();

            var console = _fx.Resolve<IColoredConsole>();
            console.WriteLine($"Completed in &#yellow#>{sw.Elapsed}");

            console.WriteLine("&#cyan#>Hello &#green#>world!!");
        }
    }
}