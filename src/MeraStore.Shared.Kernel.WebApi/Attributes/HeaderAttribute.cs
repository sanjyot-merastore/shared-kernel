namespace MeraStore.Shared.Kernel.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate | AttributeTargets.Class, AllowMultiple = true)]
    public class HeaderAttribute(string name, bool required = false, string? description = null)
        : Attribute
    {
        public string Name { get; } = name;
        public bool Required { get; } = required;
        public string? Description { get; } = description;
    }
}