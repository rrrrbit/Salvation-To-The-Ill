using UnityEngine;

[CreateAssetMenu(fileName = "Random Entity Settings", menuName = "Game/Random Entity Settings", order = 3)]
public class RandomEntitySettings : ScriptableObject
{
    public GameObject[] items;
    public AnimationCurve[] itemChances;
    [Space] 
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
    public AnimationCurve minHealth;
    public AnimationCurve maxHealth;
    public AnimationCurve healthSkew;
    [Space]
    public AnimationCurve minDefense;
    public AnimationCurve maxDefense;
    public AnimationCurve defenseSkew;
    [Space]
    public AnimationCurve minConvResistance;
    public AnimationCurve maxConvResistance;
    public AnimationCurve convResistanceSkew;
    [Space]
    public Gradient zombieColours;
    public Gradient humanColours;
    public Gradient clothesColourA;
    public Gradient clothesColourB;
}
