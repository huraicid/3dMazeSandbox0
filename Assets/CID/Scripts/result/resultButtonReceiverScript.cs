using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonReceiverScript : MonoBehaviour
{
    void Update()
    {
        // スペースキーが押されたときゲームを開始する
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X key pressed!");

            // 遷移先のシーンを読み込む
            SceneManager.LoadScene("cid_entryPoint");
        }
    }
}
