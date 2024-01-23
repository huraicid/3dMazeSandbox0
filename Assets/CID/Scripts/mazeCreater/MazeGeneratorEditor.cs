using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeGenerator mazeGenerator = (MazeGenerator)target;

        if(GUILayout.Button("���"))
        {
            mazeGenerator.GenerateMaze();
        }
        if(GUILayout.Button("����"))
        {
            mazeGenerator.ClearMaze();
        }

    }
}
#endif