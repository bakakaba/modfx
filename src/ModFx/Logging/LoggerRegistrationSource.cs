using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.Extensions.Logging;

namespace ModFx.Logging
{
    public class LoggerRegistrationSource : IRegistrationSource
    {
        public bool IsAdapterForIndividualComponents => false;

        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            //Check if is service with type
            var swt = service as IServiceWithType;
            if (swt == null)
                return Enumerable.Empty<IComponentRegistration>();

            var serviceType = swt.ServiceType;
            var serviceTypeInfo = serviceType.GetTypeInfo();
            if (!serviceTypeInfo.IsGenericType || serviceTypeInfo.GetGenericTypeDefinition() != typeof(ILogger<>))
                return Enumerable.Empty<IComponentRegistration>();

            var loggerType = serviceTypeInfo.GenericTypeArguments[0];
            var loggerSvc = swt.ChangeType(loggerType);

            var registrationCreator = CreateLoggerRegistrationMethod.MakeGenericMethod(loggerType);

            return registrationAccessor(loggerSvc)
                .Select(v => registrationCreator.Invoke(null, new object[] { service }))
                .Cast<IComponentRegistration>();
        }

        static readonly MethodInfo CreateLoggerRegistrationMethod = typeof(LoggerRegistrationSource).GetTypeInfo()
            .GetDeclaredMethod("CreateLoggerRegistration");

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
