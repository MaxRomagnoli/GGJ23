using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LookAtTarget))]
public class LookAtTargetEditor : Editor {

    public override void OnInspectorGUI ()
    {
        //base.OnInspectorGUI();
        LookAtTarget lookAtTarget = (LookAtTarget)target;

        lookAtTarget.target = (GameObject)EditorGUILayout.ObjectField("Target", lookAtTarget.target, typeof(GameObject), true);

        if(lookAtTarget.target == null)
        { EditorGUILayout.HelpBox("Assign a target to use the Look At Target script!", MessageType.Warning); return; }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Type: ");
        lookAtTarget.ifRotate = GUILayout.Toggle(lookAtTarget.ifRotate, "Fixed");
        if (lookAtTarget.ifRotate && (lookAtTarget.ifRotateSlerp || lookAtTarget.ifRotateLerp))
        {
            lookAtTarget.ifRotateLerp = false;
            lookAtTarget.ifRotateSlerp = false;
        }
        lookAtTarget.ifRotateSlerp = GUILayout.Toggle(lookAtTarget.ifRotateSlerp, "Slerp");
        if (lookAtTarget.ifRotateSlerp && (lookAtTarget.ifRotateLerp || lookAtTarget.ifRotate))
        {
            lookAtTarget.ifRotate = false;
            lookAtTarget.ifRotateLerp = false;
        }
        lookAtTarget.ifRotateLerp = GUILayout.Toggle(lookAtTarget.ifRotateLerp, "Lerp");
        if (lookAtTarget.ifRotateLerp && (lookAtTarget.ifRotateSlerp || lookAtTarget.ifRotate))
        {
            lookAtTarget.ifRotate = false;
            lookAtTarget.ifRotateSlerp = false;
        }
        GUILayout.EndHorizontal();

        if (lookAtTarget.ifRotateSlerp || lookAtTarget.ifRotateLerp)
            lookAtTarget.rotationSpeed = EditorGUILayout.FloatField("Rotation Speed", lookAtTarget.rotationSpeed);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Rotation Axis:");
        lookAtTarget.XAxis = GUILayout.Toggle(lookAtTarget.XAxis, "X");
        lookAtTarget.YAxis = GUILayout.Toggle(lookAtTarget.YAxis, "Y");
        lookAtTarget.ZAxis = GUILayout.Toggle(lookAtTarget.ZAxis, "Z");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Rotate view"))
            lookAtTarget.RotateInTargetView();
        if (GUILayout.Button("Reset view"))
            lookAtTarget.ResetRotationFromEditor();
        GUILayout.EndHorizontal();
    }
}
