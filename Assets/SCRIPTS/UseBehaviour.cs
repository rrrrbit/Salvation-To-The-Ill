using UnityEngine;

public class UseBehaviour : MonoBehaviour 
{
	public ItemData item;
    private void Start()
    {
        item = GetComponent<ItemData>();
    }

    public int Quality()
    {
        return (int)item.quality;
    }

    public virtual bool TryUse(ENTITY user, ENTITY recipient)
	{
		return false;
	}
}
