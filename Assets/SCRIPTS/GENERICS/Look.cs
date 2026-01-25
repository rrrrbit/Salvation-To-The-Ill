using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Look : MonoBehaviour
{
    public Vector2 look;
	public Vector2 displacement;
	public Vector2 shake;
    public Vector2 pitchClamp;
    public Transform cam;
    public ENTITY entity;

    public virtual void Update()
    {
        look.y = Mathf.Clamp(look.y, pitchClamp.x, pitchClamp.y) % 360;

		displacement = GLOBAL.Lerpd(displacement, Vector2.zero, 0.5f, 0.1f, Time.deltaTime);
		shake = GLOBAL.Lerpd(shake, Vector2.zero, 0.5f, 0.1f, Time.deltaTime);
		
		var shakeNow = Random.insideUnitCircle.Scaled(shake);
		var total = look + displacement.Scaled(new(1,-1)) + shakeNow;

		var r = transform.rotation;
		GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(r.eulerAngles.x, total.x, r.eulerAngles.z));
		cam.localRotation = Quaternion.Euler(total.y, 0, 0);
    }
}
