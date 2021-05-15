using UnityEngine;


public static class Extentions
{
    public static bool HasComponent<T>(this GameObject gameObject)
    {
        T[] components = gameObject.GetComponents<T>();

        if (components.Length > 0)
            return true;
        return false;
    }
}
