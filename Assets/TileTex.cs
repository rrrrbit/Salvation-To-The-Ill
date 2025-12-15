using UnityEngine;

[ExecuteAlways]
public class TileTex : MonoBehaviour
{
    [SerializeField] private float tileX = 1;
    [SerializeField] private float tileY = 1;
    Mesh mesh;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mesh = GetComponent<MeshFilter>().mesh;

    }

    void Update()
    {
        mat.mainTextureScale = new Vector2((mesh.bounds.size.x *
transform.localScale.x) * tileX, (mesh.bounds.size.y * transform.localScale.y) * tileY);
    }
}
