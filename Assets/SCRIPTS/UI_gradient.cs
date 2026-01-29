using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

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

[CustomEditor(typeof(UI_gradient))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UI_gradient myTarget = (UI_gradient)target;

        var newGradient = EditorGUILayout.GradientField("Gradient", myTarget.gradient);
        var newDirection = EditorGUILayout.Vector2Field("Direction", myTarget.direction);

        if(newGradient.alphaKeys != myTarget.gradient.alphaKeys || newGradient.colorKeys != myTarget.gradient.colorKeys || newGradient.mode != myTarget.gradient.mode || newDirection != myTarget.direction)
        {
            myTarget.gradient = newGradient;
            myTarget.direction = newDirection;
            
            myTarget.UpdateTex();
        }

        if (GUILayout.Button("reload"))
        {
            myTarget.Reload();
        }
    }
}
