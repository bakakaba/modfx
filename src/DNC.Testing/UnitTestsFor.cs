using System;
using Autofac.Extras.Moq;
using Moq;

namespace DNC.Testing
{
    public abstract class UnitTestsFor<T> : IDisposable
    {
        private AutoMock _mocker;
        protected UnitTestsFor()
        {
            _mocker = AutoMock.GetLoose();
        }

        protected Mock<TMock> Mock<TMock>() where TMock : class
        {
            return _mocker.Mock<TMock>();
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