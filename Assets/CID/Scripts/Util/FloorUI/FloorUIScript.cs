using UnityEngine;
using TMPro;

/// <summary>
/// フロア情報UIを更新するスクリプトクラスです。
/// </summary>
public class FloorUIScript : MonoBehaviour
{
    // TextMeshProオブジェクトへの参照
    public TextMeshProUGUI textMeshPro;

    /// <summary>
    /// フロア情報UIを更新します。
    /// </summary>
    public void UpdateFloorUI()
    {
        gameObject.SetActive(true);

        // TextMeshProオブジェクトが指定されていない場合は、自動的に検索する
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // テキストを更新
        UpdateText();
    }

    /// <summary>
    /// フロア情報テキストを更新します。
    /// </summary>
    void UpdateText()
    {
        // 現在のフロアを更新する
        int floor = GameVariables.floor;
        textMeshPro.text = textMeshPro.text.Replace("{%floor}", floor.ToString());

        // 最終階を更新する
        int maxFloor = GameVariables.maxFloor;
        textMeshPro.text = textMeshPro.text.Replace("{%maxFloor}", maxFloor.ToString());
        Debug.Log("updated test: " + textMeshPro.text);
    }

}