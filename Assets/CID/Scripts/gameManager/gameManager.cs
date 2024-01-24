using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // フロアをロードするときの処理
    void Start()
    {
        if(!GameVariables.isTimerActive)
        {
            GameVariables.isTimerActive = true;
        }

        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        Debug.Log("読み込みOK");

        if (GameVariables.floor < 1)
        {
            GameVariables.floor = 1;
        }

        // 迷路フロアなら迷路を生成
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene Name: " + currentSceneName);
        if (currentSceneName == "mazeFloor")
        {
            GameObject gameManagerObject = GameObject.Find("MazeManager");
            MazeGenerator script = gameManagerObject.GetComponent<MazeGenerator>();
            script.GenerateMaze();
        }

        // Floor情報のUIを更新
        updateFloorUI();

    }

    // マイフレーム実行される処理
    private void Update()
    {
        // game abort
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("cid_entryPoint");
        }

        // ゲーム経過時間を計算
        if (GameVariables.isTimerActive)
        {
            GameVariables.currentTime += Time.deltaTime;
        }
    }

    // 次のフロアに進むときの処理
    public void GoToNextFloor(string nextFloorName)
    {
        Debug.Log("isGoal:" + (GameVariables.maxFloor == GameVariables.floor));
        if(GameVariables.maxFloor == GameVariables.floor)
        {
            GameVariables.isTimerActive = false;
            SceneManager.LoadScene("result");
            return;
        }

        Debug.Log("GoToNextFloor()が呼び出されました。next: " + nextFloorName);

        // フェードアウトを開始する
        FadeOut();

        // 階数を1つ増やす
        GameVariables.floor++;

        // 遷移先のシーンを読み込む
        Debug.Log("next floor: " +  nextFloorName);
        SceneManager.LoadScene(nextFloorName);
    }

    // フェードアウトする
    private void FadeOut()
    {
        Image fadeImage = null;      // フェード用のUIパネル（Image）
        float fadeDuration = 10.0f;    // フェードの完了にかかる時間

        Debug.Log("FadeOut()が呼び出されました");

        GameObject foundObject = GameObject.Find("Image");
        // 取得したGameObjectがnullでないことを確認
        if (foundObject == null)
        {
            UnityEngine.Debug.LogWarning("指定された名前のGameObjectが見つかりませんでした。");
            return;
        }
        Debug.Log("フェードアウト用のゲームオブジェクトOK");

        // Imageコンポーネントを取得
        Image imageComponent = foundObject.GetComponent<Image>();
        if (imageComponent == null)
        {
            UnityEngine.Debug.LogWarning("GameObjectにImageコンポーネントがアタッチされていません。");
            return;
        }
        Debug.Log("フェードアウト用のImageコンポーネントOK");

        // 取得したImageコンポーネントを使用する
        fadeImage = imageComponent;

        // フェードアウトのアニメーション
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, timer / fadeDuration);
            return;
        }
    }

    // フロア情報を取得する
    private static void updateFloorUI()
    {
        GameObject floorTextObject = GameObject.Find("FloorText");
        if (floorTextObject == null)
        {
            Debug.LogWarning("floorTextが取得できませんでした");
            return;
        }
        Debug.Log("floor Text取得できました");

        //
        FloorUIScript script = floorTextObject.GetComponent<FloorUIScript>();
        if (script == null)
        {
            Debug.LogWarning("floorUIScriptが取得できませんでした");
            return;
        }

        script.updateFloorText();
    }

    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log(nextScene.name);
        Debug.Log(mode);
    }
}