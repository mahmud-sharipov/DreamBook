public static class ResourceExtensions
{
    public static string Format(this string template, params object[] args) => string.Format(template, args);
}
