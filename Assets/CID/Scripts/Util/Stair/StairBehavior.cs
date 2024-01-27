using UnityEngine;

/// <summary>
/// 階段の動作について規定するスクリプトクラスです。
/// </summary>
public class StairBehaviour : MonoBehaviour
{
    /// <summary>
    /// 階段に触れたときの動作を行います。
    /// </summary>
    /// <param name="other">触れたオブジェクト（未使用）</param>
    private void OnTriggerEnter(Collider other)
    {
        // 次のフロア名を取得する
        string targetSceneName = GetNextFloorName();

        // 次のフロアに進む（GameManagerに移譲する）
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager script = gameManagerObject.GetComponent<GameManager>();
        script.GoToNextFloor(targetSceneName);
    }

    /// <summary>
    /// 次のフロアのシーン名を取得します。
    /// </summary>
    /// <returns></returns>
    private string GetNextFloorName()
    {
        if(GameVariables.floor == 2)
        {
            return "toyBox";
        }

        return "mazeFloor";
    }
}
