using Microsoft.Extensions.Localization;
using System;

namespace Framework.Resources
{
    public class ResourceManager<TResource> : IResourceManager
    {
        private readonly IStringLocalizer<TResource> stringLocalizer;

        public ResourceManager(IStringLocalizer<TResource> stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
        }
        public string this[string name] { get => stringLocalizer[name]; }

        public string this[string name, params string[] arguments] { get => stringLocalizer[name, arguments]; }

        public string GetName(string name)
        {
            return stringLocalizer.GetString(name);
        }

        public string GetName(string name, params string[] arguments)
        {
            return stringLocalizer.GetString(name, arguments);
        }
    }
}
