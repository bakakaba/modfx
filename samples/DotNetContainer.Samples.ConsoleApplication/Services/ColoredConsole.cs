using System;

namespace DotNetContainer.Samples.ConsoleApplication.Services
{
    public class ColoredConsole : IColoredConsole
    {
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