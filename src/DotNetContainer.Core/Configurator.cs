namespace DotNetContainer.Core
{
    public class Configurator
    {
        public static T Get<T>() where T : new()
        {
            return new T();
        }
    }
}