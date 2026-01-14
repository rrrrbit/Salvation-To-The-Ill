using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Look : MonoBehaviour
{
    public Vector2 look;
    public Vector2 pitchClamp;
    public float targetPitch;
    public Transform cam;
    public ENTITY entity;

    public virtual void Update()
    {
        look.y = Mathf.Clamp(look.y, pitchClamp.x, pitchClamp.y) % 360;

        var r = transform.rotation;
		GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(r.eulerAngles.x, look.x, r.eulerAngles.z));
		cam.localRotation = Quaternion.Euler(look.y, 0, 0);
    }
}
