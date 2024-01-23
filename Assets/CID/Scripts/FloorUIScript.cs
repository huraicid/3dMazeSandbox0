using UnityEngine;
using TMPro;

public class FloorUIScript : MonoBehaviour
{
    // TextMeshProオブジェクトへの参照
    public TextMeshProUGUI textMeshPro;

    public void updateFloorText()
    {
        gameObject.SetActive(true);
        Debug.Log("Floor UI Script ON");

        // TextMeshProオブジェクトが指定されていない場合は、自動的に検索する
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // 初期のテキストを設定
        UpdateText();
    }

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