using UnityEngine;
[CreateAssetMenu(fileName = "Random Pickup Settings", menuName = "Game/Random Pickup Settings", order = 4)]
public class RandomPickupSettings : ScriptableObject
{
    public GameObject[] items;
    public AnimationCurve[] itemChances;
}
