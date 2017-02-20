namespace ModFx.Configuration
{
    public interface IConfigurationFactory
    {
        T Get<T>();
        T Get<T>(string moduleName);
    }
}