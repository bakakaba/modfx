using ModFx.Samples.ConsoleApplication.Services;

namespace ModFx.Samples.ConsoleApplication
{
    class Program
    {
        static Framework _fx;

        static void Main(string[] args)
        {
            _fx = Framework.Initialize();
            var console = _fx.Resolve<IColoredConsole>();

            console.WriteLine("&#cyan#>Hello &#green#>world!!");
        }
    }
}