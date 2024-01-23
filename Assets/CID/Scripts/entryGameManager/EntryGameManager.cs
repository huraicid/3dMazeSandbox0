using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryGameManager : MonoBehaviour
{
    void Update()
    {
        // スペースキーが押されたときゲームを開始する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed!");

            // 各種データを初期化する
            GameVariables.init();

            // 遷移先のシーンを読み込む
            SceneManager.LoadScene("mazeFloor");
        }

        // qキーが押されたときゲームを終了する
        if(Input.GetKeyDown(KeyCode.Q)) {
            QuitGame();
        }
    }

    /// <summary>
    /// ゲームを終了します。
    /// </summary>
    /// <remarks>
    /// Unity Editorか実際の実行資材かでゲームを終了する処理が違うため、
    /// ビルド時にどのような処理を実施するか、実装のように指定する必要があります。
    /// （インデントを含めて重要です））
    /// </remarks>
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }
}
