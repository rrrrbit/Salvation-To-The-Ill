using System.Linq;
using UnityEngine;

public class OBJ_pickup : MonoBehaviour
{
    public GameObject item;
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
    }
}
