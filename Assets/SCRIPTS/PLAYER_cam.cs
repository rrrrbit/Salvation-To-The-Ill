using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PLAYER_cam : MonoBehaviour
{
    public static PLAYER_cam instance;

    [SerializeField] float mouseSensitivityMult;
    [SerializeField] Vector2 mouseSensitivity;
    public Vector2 pitchClamp;

    public float targetPitch;

    public Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        var md = Mouse.current.delta.ReadValue() * mouseSensitivity * mouseSensitivityMult;
        targetPitch = Mathf.Clamp(targetPitch - md.y, pitchClamp.x, pitchClamp.y) % 360;

        var r = transform.rotation;
		GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y + md.x, r.eulerAngles.z));
		cam.transform.localRotation = Quaternion.Euler(targetPitch, 0, 0);
    }
	


}
