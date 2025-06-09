namespace MeraStore.Shared.Kernel.Helpers;

public static class GeneralHelpers
{
    /// <summary>
    /// Safely casts an object to specified type or returns default.
    /// </summary>
    public static T SafeCast<T>(this object? obj, T defaultValue = default!)
    {
        return obj is T t ? t : defaultValue;
    }

       

    /// <summary>
    /// Deep clones an object using JSON serialization.
    /// </summary>
    public static T DeepClone<T>(this T obj)
    {
        if (obj == null) return default!;

        var json = JsonHelper.Serialize(obj);
        return JsonHelper.Deserialize<T>(json)!;
    }
}