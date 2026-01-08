using UnityEngine;

public class OBJ_Grenade : MonoBehaviour
{
    public float lifetime;
    public GameObject explosion;
    public float gravityMult;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * (gravityMult - 1), ForceMode.Force);
    }

    private void OnDestroy()
    {
        foreach(var obj in GetComponentsInChildren<Transform>())
        {
            obj.SetParent(transform.parent);
        }
    }
}
