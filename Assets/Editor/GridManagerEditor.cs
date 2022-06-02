using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridManager mapGen = (GridManager)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            mapGen.Init();
        }
    }
}
