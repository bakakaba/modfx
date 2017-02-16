using Autofac;

namespace DotNetContainer.Samples.ConsoleApplication
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}