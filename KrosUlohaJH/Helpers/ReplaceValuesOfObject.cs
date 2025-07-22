using System.Reflection;

namespace KrosUlohaJH.Helpers
{
    public static class ReplaceValuesOfObject
    {
        /// <summary>
        /// Funkcia by mala nahradiť všetky polia, ktoré neboli prázdne v objekte.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Objekt z ktorého nahrádzam</param>
        /// <param name="target">Objekt ktorý chcem meniť</param>
        /// <param name="excludedProperties">Property ktoré chcem excludnúť, tie kontrolujem manuálne.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateNonNullProperties<T>(T source, T target, string[]? excludedProperties = null)
        {
            if (source == null || target == null)
                throw new ArgumentNullException("Source and target must not be null.");

            var exclusions = excludedProperties?.ToHashSet() ?? new HashSet<string>();
            // Načítavam properties
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && !exclusions.Contains(p.Name));

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source);


                if (value == null)
                    continue;


                if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                    continue;

                prop.SetValue(target, value);
            }
        }
    }
}
