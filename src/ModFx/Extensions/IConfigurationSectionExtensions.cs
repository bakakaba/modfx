using Microsoft.Extensions.Configuration;

namespace ModFx.Extensions
{
    public static class IConfigurationSectionExtensions
    {
        public static IConfigurationSection Merge(
            this IConfigurationSection baseSection,
            IConfigurationSection inheritedSection)
        {
            foreach (var item in inheritedSection.AsEnumerable())
            {
                var ix = item.Key.IndexOf(':');
                if (ix == -1)
                    continue;

                var key = item.Key.Substring(ix + 1);
                if (baseSection[key].IsNullOrWhiteSpace())
                    baseSection[key] = item.Value;
            }

            return baseSection;
        }
    }
}