using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ダンジョン内のゲーム処理を管理するクラスです。
/// </summary>
public class GameManager : MonoBehaviour
{
    // フロアをロードするときの処理
    void Start()
    {
        // タイマーが起動されていなければ起動する
        if(!GameVariables.isTimerActive)
        {
            GameVariables.isTimerActive = true;
        }

        // フロア情報が初期化前なら初期化する
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
        UpdateFloorUI();

    }

    // マイフレーム実行される処理
    private void Update()
    {
        // Escキーが押されたときゲームを終了する
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameVariables.isTimerActive = false;
            SceneManager.LoadScene("cid_entryPoint");
        }

        // ゲーム経過時間を計算
        if (GameVariables.isTimerActive)
        {
            GameVariables.currentTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// 次のフロアに進むときの処理です。
    /// </summary>
    /// <param name="nextFloorName">次のフロアの名前</param>
    public void GoToNextFloor(string nextFloorName)
    {
        // 最上階から次のフロアに進むときはクリア画面に遷移する
        if(GameVariables.maxFloor == GameVariables.floor)
        {
            GameVariables.isTimerActive = false;
            SceneManager.LoadScene("result");
            return;
        }

        // フェードアウトを開始する
        FadeOut();

        // 階数を1つ増やす
        GameVariables.floor++;

        // 遷移先のシーンを読み込む
        Debug.Log("next floor: " +  nextFloorName);
        SceneManager.LoadScene(nextFloorName);
    }

    /// <summary>
    /// フェードアウトします。
    /// </summary>
    private void FadeOut()
    {
        Image fadeImage = null;      // フェード用のUIパネル（Image）
        float fadeDuration = 10.0f;    // フェードの完了にかかる時間

        GameObject foundObject = GameObject.Find("Image");
        // 取得したGameObjectがnullでないことを確認
        if (foundObject == null)
        {
            UnityEngine.Debug.LogWarning("指定された名前のGameObjectが見つかりませんでした。");
            return;
        }

        // Imageコンポーネントを取得
        Image imageComponent = foundObject.GetComponent<Image>();
        if (imageComponent == null)
        {
            UnityEngine.Debug.LogWarning("GameObjectにImageコンポーネントがアタッチされていません。");
            return;
        }

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

    /// <summary>
    /// フロア情報を取得します。
    /// </summary>
    private static void UpdateFloorUI()
    {
        // Floor UIを取得してアタッチしたScriptを実行
        GameObject floorTextObject = GameObject.Find("FloorText");
        if (floorTextObject == null)
        {
            Debug.LogWarning("floorTextが取得できませんでした");
            return;
        }

        FloorUIScript script = floorTextObject.GetComponent<FloorUIScript>();
        if (script == null)
        {
            Debug.LogWarning("floorUIScriptが取得できませんでした");
            return;
        }
        script.UpdateFloorUI();
    }
}