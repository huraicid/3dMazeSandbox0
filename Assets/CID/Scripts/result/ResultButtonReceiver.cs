using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonReceiver : MonoBehaviour
{
    void Update()
    {
        // Xキーが押されたときタイトル画面に戻る
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X key pressed!");

            // 遷移先のシーンを読み込む
            SceneManager.LoadScene("cid_entryPoint");
        }
    }
}
