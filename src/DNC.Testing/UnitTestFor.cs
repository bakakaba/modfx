using System;
using Autofac;
using Autofac.Extras.Moq;
using Moq;

namespace DNC.Testing
{
    public abstract class UnitTestFor<T> : IDisposable
    {
        private AutoMock _mocker;
        protected UnitTestFor()
        {
            _mocker = AutoMock.GetLoose();
        }

        protected Mock<TMock> Mock<TMock>() where TMock : class
        {
            return _mocker.Mock<TMock>();
        }

        protected void RegisterInstance<TInstance>(TInstance instance) where TInstance : class
        {
            var builder = new ContainerBuilder();
            //Ignore exception here, it's a known bug (http://stackoverflow.com/questions/30039040/project-referencing-portable-class-library-gives-error-in-visual-studio-code).
            builder.RegisterInstance<TInstance>(instance);
            builder.Update(_mocker.Container);
        }

        protected T Component
        {
            get
            {
                return _mocker.Create<T>();
            }
        }

        public void Dispose()
        {
            _mocker.Dispose();
        }
    }
}