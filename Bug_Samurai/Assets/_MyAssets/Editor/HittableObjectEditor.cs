using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BreakableObject))]
public class HittableEditor : Editor
{

    public override void OnInspectorGUI()
    {
        BreakableObject breakableObject = (BreakableObject)target;
        base.OnInspectorGUI();
    }
}
