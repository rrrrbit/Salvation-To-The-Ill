using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    InpActions.PlayerActions actions;
    Rigidbody rb;
    Collider col;

    [SerializeField] float xSpeed;
    [SerializeField] float xMvtLerpK;
    [SerializeField] float xMvtLerpT;

    [SerializeField]
    public Rigidbody groundCheck;
    public bool grounded;

    public TextMeshProUGUI debug;

    Vector3 horizontalVel;

    void Start()
    {
        var inp = new InpActions();
        inp.Enable();
        actions = inp.Player;
        actions.Enable();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var mvtIn = actions.move.ReadValue<Vector2>();
        var targetVel = transform.rotation * new Vector3(mvtIn.x, 0, mvtIn.y) * xSpeed;
        horizontalVel = GLOBAL.Lerpd(horizontalVel, targetVel, xMvtLerpK, xMvtLerpT, Time.deltaTime) ;
        rb.linearVelocity = new Vector3(horizontalVel.x, rb.linearVelocity.y, horizontalVel.z);

        RaycastHit hitInfo;
        
        grounded = groundCheck.SweepTest(-transform.up, out hitInfo, 1f) && hitInfo.distance <= 0.501f;
        debug.text = "grounded: "+grounded.ToString() + "\n";
        if(grounded)
        {
            print(hitInfo.distance);
            debug.text += hitInfo.distance.ToString();
        }

        if(actions.Jump.IsPressed() && grounded)
        {
            //jump
        }
    }
}
