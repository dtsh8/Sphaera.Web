using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Sphaera.Web.Api.Resources;

namespace Sphaera.Web.Api.Helpers
{
    public static class ResourcesHelper
    {
        private static readonly ConcurrentDictionary<string, Dictionary<string, string>> Resources = new ConcurrentDictionary<string, Dictionary<string, string>>();

        public static Dictionary<string, string> LoadResources(this Assembly assem, string resourcesName)
        {
            return Resources.GetOrAdd(resourcesName, s =>
            {
                using (var resourceStream = assem.GetManifestResourceStream(s))
                {
                    if (resourceStream == null)
                        throw new InvalidOperationException(string.Format(Errors.CanntLoadResource, s));

                    var resources = new Dictionary<string, string>();

                    using (var resourceReader = new ResourceReader(resourceStream))
                    {
                        var dict = resourceReader.GetEnumerator();
                        while (dict.MoveNext())
                            resources.Add((string) dict.Key, (string) dict.Value);

                        resourceReader.Close();
                    }

                    resourceStream.Close();

                    return resources;
                }
            });
        }
    }
}