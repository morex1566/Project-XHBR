using UnityEngine;

public static class Utls
{
    /// <summary>
    /// Return target's type name.
    /// </summary>
    /// <typeparam name="T"> target's type(Optional) </typeparam>
    /// <param name="value"> target </param>
    /// <returns> target's type </returns>
    public static string GetTypeToString<T>(T value)
    {
        return value.GetType().Name;
    }

    /// <summary>
    /// Throw exception and log about missing error.
    /// </summary>
    /// <typeparam name="T"> Missing error target's type(Optional) </typeparam>
    /// <param name="value"> Missing error target </param>
    /// <param name="valueName"> Missing error target's name </param>
    public static void throwMissingError<T>(T value, string valueName)
    {
        string errorMsg = $"{value.GetType().Name} : variable '{valueName}' is missing.";
        Debug.LogError(errorMsg);
        throw new System.Exception(errorMsg);
    }
}