using System;
using Autofac;
using Autofac.Extras.Moq;
using ModFx.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace ModFx.Testing
{
    public abstract class UnitTestFor<T> : IDisposable
    {
        private AutoMock _mocker;

        protected UnitTestFor()
        {
            _mocker = AutoMock.GetLoose();
            ConfigureLogger();
        }

        public void Dispose() => _mocker.Dispose();

        protected T Component => _mocker.Create<T>();

        protected Mock<TMock> Mock<TMock>() where TMock : class
        {
            return _mocker.Mock<TMock>();
        }

        protected void Register<TService, TImplementation>()
        {
            _mocker.Provide<TService, TImplementation>();
        }

        protected void RegisterInstance<TInstance>(TInstance instance) where TInstance : class
        {
            _mocker.Provide<TInstance>(instance);
        }

        private void ConfigureLogger()
        {
            Log.Logger = new Serilog.LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}")
                .CreateLogger();

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog();

            RegisterInstance<ILoggerFactory>(loggerFactory);

            var builder = new ContainerBuilder();
            builder.RegisterSource(new LoggerRegistrationSource());
            builder.Update(_mocker.Container);
        }
    }
}