using UnityEngine;
using UnityEditor;

/// <summary>
/// 手動で迷路生成ができるように、オブジェクトにアタッチするスクリプトクラスです。
/// </summary>
#if UNITY_EDITOR
[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorEditor : Editor
{
    /// <summary>
    /// Inspectorに表示する項目です。
    /// </summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeGenerator mazeGenerator = (MazeGenerator)target;

        if(GUILayout.Button("作る"))
        {
            // 迷路を生成
            mazeGenerator.GenerateMaze();
        }
        if(GUILayout.Button("消す"))
        {
            // 迷路を消去
            mazeGenerator.ClearMaze();
        }

    }
}
#endif