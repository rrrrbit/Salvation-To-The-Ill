using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(UI_gradient))]
public class LevelScriptEditor : Editor
{
	public override void OnInspectorGUI()
	{
		UI_gradient myTarget = (UI_gradient)target;

		var newGradient = EditorGUILayout.GradientField("Gradient", myTarget.gradient);
		var newDirection = EditorGUILayout.Vector2Field("Direction", myTarget.direction);

		if (newGradient.alphaKeys != myTarget.gradient.alphaKeys || newGradient.colorKeys != myTarget.gradient.colorKeys || newGradient.mode != myTarget.gradient.mode || newDirection != myTarget.direction)
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
