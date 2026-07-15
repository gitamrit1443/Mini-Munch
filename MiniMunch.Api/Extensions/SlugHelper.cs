using System.Text.RegularExpressions;

namespace MiniMunch.Web.Extensions;

public static class SlugHelper
{
    public static string ToSlug(string value)
    {
        value = value.Trim().ToLowerInvariant();
        value = Regex.Replace(value, @"[^a-z0-9\s-]", "");
        value = Regex.Replace(value, @"\s+", "-");
        value = Regex.Replace(value, @"-+", "-");
        return value.Trim('-');
    }
}
