using UnityEngine;

public class UseBehaviour : MonoBehaviour 
{
	public ItemData item;
    private void Start()
    {
        item = GetComponent<ItemData>();
    }

    protected int Quality()
    {
        return (int)item.quality;
    }

    public virtual bool TryUse(ENTITY entity)
	{
		return false;
	}
}
