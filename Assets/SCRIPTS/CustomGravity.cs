using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    [SerializeField] bool multiply;
    [SerializeField] Vector3 gravity;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().useGravity = false;
        if (multiply)
        {
            GetComponent<Rigidbody>().AddForce(Physics.gravity.Scaled(gravity));
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(gravity);
        }
        
    }
}
