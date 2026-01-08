using TMPro;
using UnityEngine;

public class VFX_dmgText : MonoBehaviour
{
	TextMeshPro tmp;
	public bool flashing;
	public float fadeTime;
	public Color c1;
	public Color c2;
	public float flashInterval;
	public bool destroyAfter;
	public float lifetime;

	float flashTimer;
	float fadeTimer;
	bool c;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		fadeTimer = fadeTime;
		tmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
		if(lifetime <= 0 ) fadeTimer -= Time.deltaTime;

		if(fadeTimer <= 0 && destroyAfter) Destroy(gameObject);

		if(flashing) flashTimer -= Time.deltaTime;
		if (flashTimer <= 0)
		{
			c = !c;
			flashTimer = flashInterval;
		}

		var C = c ? c1 : c2;
		tmp.color = new(C.r, C.g, C.b, fadeTimer / fadeTime);

		transform.forward = PLYR.cam.cam.transform.forward;
    }
}
