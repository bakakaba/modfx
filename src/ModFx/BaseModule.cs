using System.Reflection;
using Autofac;

namespace ModFx
{
    public abstract class BaseModule : Autofac.Module
    {
        protected override Assembly ThisAssembly => GetType().GetTypeInfo().Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = ThisAssembly;

            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .AsSelf();

            Configure(builder);
        }

        protected abstract void Configure(ContainerBuilder builder);
    }

    public abstract class BaseModule<T> : BaseModule where T : class, new()
    {
        protected T Configuration { get; } = Framework.Instance.ConfigurationFactory.Get<T>();

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(Configuration);
            base.Load(builder);
        }
    }
}