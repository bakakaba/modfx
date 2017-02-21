using System;

namespace ModFx.Configuration
{
    public interface IConfigurationFactory
    {
        /// <summary>
        /// Gets the configuration object from the configuration root based on the type.
        /// </summary>
        /// <returns>Configuration object.</returns>
        T Get<T>();

        /// <summary>
        /// Gets the configuration object from the configuration root based on the type.
        /// </summary>
        /// <param name="inheritedModuleTypes">
        ///     Other configuration types to inherit from.
        ///     Cascades from first to last.
        /// </param>
        /// <returns>Configuration object.</returns>
        T Get<T>(params Type[] inheritedModuleTypes);

        /// <summary>
        /// Gets the configuration object from the configuration root.
        /// </summary>
        /// <param name="moduleName">The namespace to the configuration object.</param>
        /// <param name="inheritedModuleNames">
        ///     Other configuration sections to inherit from.
        ///     Cascades from first to last.
        /// </param>
        /// <returns>Configuration object.</returns>
        T Get<T>(string moduleName, params string[] inheritedModuleNames);
    }
}