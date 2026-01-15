using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Game/Weapon Stats", order = 2)]
public class WeaponStats : ScriptableObject
{
    public Vector2[] dmgRange;
    public Vector2[] conversionRange;
    public Vector2[] spread;
    public int[] penetration;
    public float[] costAmmo;
    public float[] costHealth;
    public float[] shootInterval;
    public float[] bulletSpeed;
    public int[] bullets;
    public float[] effectiveRange;
    public bool gravity;
    public bool targetOriginLayer;
}
