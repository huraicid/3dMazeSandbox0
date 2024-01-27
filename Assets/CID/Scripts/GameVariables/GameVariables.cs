/// <summary>
/// ゲーム内で共通で使用するデータクラスです。
/// </summary>
public static class GameVariables
{
    /// <summary>
    /// 現在のフロア
    /// </summary>
    public static int floor = 0;

    /// <summary>
    /// ダンジョンの最上階のフロア
    /// </summary>
    public static int maxFloor = 4;
    

    /// <summary>
    /// タイマー起動フラグ
    /// </summary>
    public static bool isTimerActive = false;

    /// <summary>
    /// 現在のタイマー
    /// </summary>
    public static float currentTime = 0f;


    /// <summary>
    /// データをすべて初期化します。
    /// </summary>
    public static void Init()
    {
        floor = 0;
        isTimerActive = false;
        currentTime = 0f;
    }
}
