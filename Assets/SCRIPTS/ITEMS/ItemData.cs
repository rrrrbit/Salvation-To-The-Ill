using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public Sprite itemSprite;
    public GameObject itemModel;
    public string itemName;
    public float defaultRange;
    public enum Qualities
    {
        shoddy = 0,
        average = 1,
        decent = 2,
        pristine = 3,
        advanced = 4,
    }
    public Qualities quality = Qualities.average;
    public int amt = 1;
    public int maxStack = 8;
}
