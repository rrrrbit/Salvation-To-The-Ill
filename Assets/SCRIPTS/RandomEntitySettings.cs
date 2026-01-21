using UnityEngine;

[CreateAssetMenu(fileName = "Random Entity Settings", menuName = "Game/Random Entity Settings", order = 3)]
public class RandomEntitySettings : ScriptableObject
{
    public AnimationCurve[] qualityChances;
    [Space]
    public AnimationCurve minSpeed;
    public AnimationCurve maxSpeed;
    public AnimationCurve speedSkew;
    [Space]
    public AnimationCurve minSize;
    public AnimationCurve maxSize;
    public AnimationCurve sizeSkew;
    [Space]
    public AnimationCurve healthSkew;
    public AnimationCurve defenseSkew;
    public AnimationCurve convResistanceSkew;
    [Space]
    public Gradient zombieColours;
    public Gradient humanColours;
    public Gradient clothesColourA;
    public Gradient clothesColourB;
}
