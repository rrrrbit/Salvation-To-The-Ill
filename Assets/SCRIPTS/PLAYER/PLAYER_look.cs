using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PLAYER_look : Look
{
    public float mouseSensitivityMult;
    public Vector2 mouseSensitivity;
	public float targetFov;
	public Camera mainCam;

	void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		targetFov = mainCam.fieldOfView;

	}

    public override void Update()
    {
        look += (Mouse.current.delta.ReadValue() * mouseSensitivity * mouseSensitivityMult).Scaled(new(1,-1));
		mainCam.fieldOfView = GLOBAL.Lerpd(mainCam.fieldOfView, targetFov, 0.75f, 0.05f, Time.deltaTime);
        base.Update(); 
    }
}
