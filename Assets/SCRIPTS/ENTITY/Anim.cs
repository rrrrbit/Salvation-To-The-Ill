using UnityEngine;

public class Anim : MonoBehaviour
{
    Animator anim;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        if(TryGetComponent(out Movement mvt))
		{
			
		}
	}
}
