using System;
using Microsoft.Extensions.Logging;

namespace ModFx.Samples.ConsoleApplication.Services
{
    public class ColoredConsole : IColoredConsole
    {
        private readonly ILogger _logger;

        public ColoredConsole(ILogger<ColoredConsole> logger)
        {
            _logger = logger;
        }

        public void Write(string text)
        {
            Process(text);
        }

        public void WriteLine(string text)
        {
            Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        private void Process(string text)
        {
            var parts = text.Split(new [] { "&#" }, StringSplitOptions.None);
            foreach (var part in parts)
            {
                var t = part.Split(new [] { "#>" }, StringSplitOptions.None);
                if (t.Length < 2)
                {
                    Console.Write(t[0]);
                    continue;
                }

                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), t[0], true);
                Console.Write(t[1]);
            }
        }
    }
}