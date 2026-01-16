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
        shoddy,
        average,
        decent,
        pristine,
        advanced        
    }
    public Qualities quality = Qualities.average;
    public int amt = 1;
}
