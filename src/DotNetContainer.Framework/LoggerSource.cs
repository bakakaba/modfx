using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using DotNetContainer.Validation;
using DotNetContainer.Validation.Validators;
using Microsoft.Extensions.Logging;

namespace DotNetContainer.Framework
{
    public class LoggerSource : IRegistrationSource
    {
        public bool IsAdapterForIndividualComponents => false;

        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            Require.That(service).IsNotNull();
            Require.That(registrationAccessor).IsNotNull();

            //Check if is service with type
            var swt = service as IServiceWithType;
            if (swt == null)
                return Enumerable.Empty<IComponentRegistration>();

            var serviceType = swt.ServiceType;
            var serviceTypeInfo = serviceType.GetTypeInfo();
            if (!serviceTypeInfo.IsGenericType || serviceTypeInfo.GetGenericTypeDefinition() != typeof(ILogger<>))
                return Enumerable.Empty<IComponentRegistration>();

            var loggerType = serviceTypeInfo.GetGenericArguments()[0];
            var loggerSvc = swt.ChangeType(loggerType);

            var registrationCreator = CreateLoggerRegistrationMethod.MakeGenericMethod(loggerType);

            return registrationAccessor(loggerSvc)
                .Select(v => registrationCreator.Invoke(null, new object[] { service }))
                .Cast<IComponentRegistration>();
        }

        static readonly MethodInfo CreateLoggerRegistrationMethod = typeof(LoggerSource).GetTypeInfo()
            .GetMethod("CreateLoggerRegistration", BindingFlags.Static | BindingFlags.NonPublic);

        static IComponentRegistration CreateLoggerRegistration<T>(Service service)
        {
            var rb = RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    var loggerFactory = c.Resolve<ILoggerFactory>();
                    return new Logger<T>(loggerFactory);
                })
                .As(service);

            return rb.CreateRegistration();
        }
    }
}
