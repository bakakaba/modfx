using System.Reflection;
using Autofac;

namespace DotNetContainer.Core
{
    public abstract class BaseModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .AsSelf();

            Register(builder);
        }

        protected abstract void Register(ContainerBuilder builder);

        protected override Assembly ThisAssembly
        {
            get
            {
                return GetType().GetTypeInfo().Assembly;
            }
        }
    }

    public abstract class BaseModule<T> : BaseModule where T : new()
    {
        private T _configuration;
        protected T Configuration
        {
            get
            {
                if (_configuration == null)
                    _configuration = Configurator.Get<T>();

                return _configuration;
            }
        }
    }
}