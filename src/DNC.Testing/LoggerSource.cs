using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using DNC.Validation;
using DNC.Validation.Validators;
using Microsoft.Extensions.Logging;

public class LoggerSource : IRegistrationSource
{
    public bool IsAdapterForIndividualComponents => false;

    public IEnumerable<IComponentRegistration> RegistrationsFor(
        Service service,
        Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
    {
        Require.That(service).IsNotNull();
        Require.That(registrationAccessor).IsNotNull();

        var swt = service as IServiceWithType;
        if (swt == null
            || !swt.ServiceType.IsGenericType
            || swt.ServiceType.GetGenericTypeDefinition() != typeof(ILogger<>))
            return Enumerable.Empty<IComponentRegistration>();

        var loggerType = swt.ServiceType.GetGenericArguments()[0];
        var loggerSvc = swt.ChangeType(loggerType);

        var registrationCreator = CreateLoggerRegistrationMethod.MakeGenericMethod(loggerType);

        return registrationAccessor(loggerSvc)
            .Select(v => registrationCreator.Invoke(null, new object[] { service }))
            .Cast<IComponentRegistration>();
    }

    static readonly MethodInfo CreateLoggerRegistrationMethod = typeof(LoggerSource)
        .GetMethod("CreateLoggerRegistration", BindingFlags.Static | BindingFlags.NonPublic);

    static IComponentRegistration CreateLoggerRegistration<T>(Service service)
    {
        var rb = RegistrationBuilder.ForDelegate(
            (c, p) => {
                var loggerFactory = c.Resolve<ILoggerFactory>();
                return new Logger<T>(loggerFactory);
            })
            .As(service);

        return RegistrationBuilder.CreateRegistration(rb);
    }

}