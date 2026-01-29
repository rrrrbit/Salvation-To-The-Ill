using System;
using UnityEditor;
using UnityEngine;

public static class GLOBAL
{
    public static float SQRT2OVER2 = Mathf.Sqrt(2) / 2;

    public static float Lerpd(float a, float b, float k, float t, float d)
    {
        return Mathf.Lerp(
            a, b,
            1 - Mathf.Pow(
                1 - k,
                d / t));
    }
    public static float LerpAngleD(float a, float b, float k, float t, float d)
    {
        return Mathf.LerpAngle(
            a, b,
            1 - Mathf.Pow(
                1 - k,
                d / t));
    }

    public static Vector2 Lerpd(Vector2 a, Vector2 b, float k, float t, float d)
    {
        return Vector2.Lerp(
            a, b,
            1 - Mathf.Pow(
                1 - k,
                d / t));
    }
    public static Vector3 Lerpd(Vector3 a, Vector3 b, float k, float t, float d)
    {
        return Vector3.Lerp(
            a, b,
            1 - Mathf.Pow(
                1 - k,
                d / t));
    }
    public static Vector3 Slerpd(Vector3 a, Vector3 b, float k, float t, float d)
    {
        return Vector3.SlerpUnclamped(
            a, b,
            1 - Mathf.Pow(
                1 - k,
                d / t));
    }
    public static float Lerpd(float a, float b, float f, float d)
    {
        return Mathf.Lerp(
            a, b,
            1 - Mathf.Pow(f, d));
    }
    public static float LerpAngleD(float a, float b, float f, float d)
    {
        return Mathf.LerpAngle(
            a, b,
            1 - Mathf.Pow(f, d));
    }
    public static Vector2 Lerpd(Vector2 a, Vector2 b, float f, float d)
    {
        return Vector2.Lerp(
            a, b,
            1 - Mathf.Pow(f, d));
    }
    public static Vector3 Lerpd(Vector3 a, Vector3 b, float f, float d)
    {
        return Vector3.Lerp(
            a, b,
            1 - Mathf.Pow(f, d));
    }
    public static Vector3 Slerpd(Vector3 a, Vector3 b, float f, float d)
    {
        return Vector3.SlerpUnclamped(
            a, b,
            1 - Mathf.Pow(f, d));
    }
    public static float LerpdF(float k, float t)
    {
        return Mathf.Pow(1 - k, 1 / t);
    }
    public static void DrawCross(Vector3 pos, float size = 10, Color? color = null)
    {
        var c = color ?? Color.white;

        Debug.DrawLine(pos + Vector3.left * size / 2, pos + Vector3.right * size / 2, c);
        Debug.DrawLine(pos + Vector3.up * size / 2, pos + Vector3.down * size / 2, c);
    }

    public static void DrawBounds(Bounds bounds, Color? color = null)
    {
        if (!color.HasValue) { color = Color.white; }

        Debug.DrawLine(new(bounds.min.x, bounds.min.y), new(bounds.min.x, bounds.max.y), (Color)color);
        Debug.DrawLine(new(bounds.max.x, bounds.min.y), new(bounds.max.x, bounds.max.y), (Color)color);
        Debug.DrawLine(new(bounds.min.x, bounds.min.y), new(bounds.max.x, bounds.min.y), (Color)color);
        Debug.DrawLine(new(bounds.min.x, bounds.max.y), new(bounds.max.x, bounds.max.y), (Color)color);
    }

    public static bool Contains(this LayerMask mask, GameObject gameObject)
    {
        return (mask & (1 << gameObject.layer)) != 0;
    }
    public static Vector2 xz(this Vector3 v) => new(v.x, v.z);

    public static Vector3 xz(this Vector2 v, float y) => new(v.x, y, v.y);

    public static Vector2 Scaled(this Vector2 v, Vector2 other)
    {
        v.Scale(other);
        return v;
    }

    public static Vector3 Scaled(this Vector3 v, Vector3 other)
    {
        v.Scale(other);
        return v;
    }

    public static Vector3 DivideBy(this Vector3 v, Vector3 other) => new(v.x / other.x, v.y / other.y, v.z / other.z);
    public static Vector2 DivideBy(this Vector2 v, Vector2 other) => new(v.x / other.x, v.y / other.y);


    public static bool IsEmpty<T>(this T[] self)
	{
		if (self.Length == 0) return true;
		foreach (T item in self) if (item != null) return false;
		return true;
	}

    public static void CheckChange<T>(this T self, ref T other, Action callback)
    {
        if(!self.Equals(other))
        {
            other = self;
            callback();
        }
    }
}

public static class Mathv
{
    public static Vector2Int RoundToInt(Vector2 v) => new(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
}

public static class TextureScaler
{

    /// <summary>
    /// Returns a scaled copy of given texture. 
    /// </summary>
    /// <param name="tex">Source texure to scale</param>
    /// <param name="width">Destination texture width</param>
    /// <param name="height">Destination texture height</param>
    /// <param name="mode">Filtering mode</param>
    public static Texture2D scaled(this Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0, 0, width, height);
        _gpu_scale(src, width, height, mode);

        //Get rendered data back to a new texture
        Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
        result.Reinitialize(width, height);
        result.ReadPixels(texR, 0, 0, true);
        return result;
    }

    /// <summary>
    /// Scales the texture data of the given texture.
    /// </summary>
    /// <param name="tex">Texure to scale</param>
    /// <param name="width">New width</param>
    /// <param name="height">New height</param>
    /// <param name="mode">Filtering mode</param>
    public static void scale(Texture2D tex, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0, 0, width, height);
        _gpu_scale(tex, width, height, mode);

        // Update new texture
        tex.Reinitialize(width, height);
        tex.ReadPixels(texR, 0, 0, true);
        tex.Apply(true);    //Remove this if you hate us applying textures for you :)
    }

    // Internal unility that renders the source texture into the RTT - the scaling method itself.
    static void _gpu_scale(Texture2D src, int width, int height, FilterMode fmode)
    {
        //We need the source texture in VRAM because we render with it
        src.filterMode = fmode;
        src.Apply(true);

        //Using RTT for best quality and performance. Thanks, Unity 5
        RenderTexture rtt = new RenderTexture(width, height, 32);

        //Set the RTT in order to render to it
        Graphics.SetRenderTarget(rtt);

        //Setup 2D matrix in range 0..1, so nobody needs to care about sized
        GL.LoadPixelMatrix(0, 1, 1, 0);

        //Then clear & draw the texture to fill the entire RTT.
        GL.Clear(true, true, new Color(0, 0, 0, 0));
        Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
    }
}