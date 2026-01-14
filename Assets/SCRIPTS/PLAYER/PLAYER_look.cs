using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PLAYER_look : Look
{
    [SerializeField] float mouseSensitivityMult;
    [SerializeField] Vector2 mouseSensitivity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Update()
    {
        look += (Mouse.current.delta.ReadValue() * mouseSensitivity * mouseSensitivityMult).ComponentWiseMult(new(1,-1));
        base.Update(); 
    }
}
