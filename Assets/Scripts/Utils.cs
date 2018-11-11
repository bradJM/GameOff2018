using UnityEngine;

public static class Utils
{
    public static void Flip(Transform transform)
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
