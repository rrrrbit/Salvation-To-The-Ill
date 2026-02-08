using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class UI_gradient : MonoBehaviour
{
    public Gradient gradient;
    public Vector2 direction;

    public Gradient lastGradient;
    public Vector2 lastDirection;
    public void Awake()
    {
        Reload();
    }

    private void Update()
    {
        gradient.CheckChange(ref lastGradient, UpdateTex);
        direction.CheckChange(ref lastDirection, UpdateTex);
    }

    public void UpdateTex()
    {
        TextureGradient grad = new TextureGradient(gradient);
        Texture2D tex = grad.GetTexture();

        GetComponent<UnityEngine.UI.Image>().material.SetTexture("_Texture2D", tex);
        GetComponent<UnityEngine.UI.Image>().material.SetVector("_Direction", direction);
    }

    public void Reload()
    {
        lastGradient = gradient;
        lastDirection = direction;
        GetComponent<UnityEngine.UI.Image>().material = new Material(GetComponent<UnityEngine.UI.Image>().material);
        UpdateTex();
        print("reload");
    }
}


