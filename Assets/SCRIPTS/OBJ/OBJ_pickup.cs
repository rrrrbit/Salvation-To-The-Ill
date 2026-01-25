using System.Linq;
using TMPro;
using UnityEngine;

public class OBJ_pickup : MonoBehaviour
{
    public GameObject item;
    public TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(item.GetComponent<ItemData>().amt == 0)
        {
            Destroy(gameObject);
        }
        text.text = item.GetComponent<ItemData>().itemName;
        text.transform.forward = PLYR.player.look.cam.transform.forward;
		text.color = HUD.hud.qualityColours[(int)item.GetComponent<ItemData>().quality];
    }
}
