using TMPro;
using UnityEngine;

/// <summary>
/// クリア時間を更新するスクリプトクラスです。
/// </summary>
public class CrearTimeUpdater : MonoBehaviour
{
    
    void Start()
    {
        // 時刻をxx:yy.zz表記で出力する
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        float currentTime = GameVariables.currentTime;
        textMeshPro.text = string.Format("{0:D2}:{1:D2}.{2:D2}",
            (int)currentTime / 60,
            (int)currentTime % 60,
            (int)(currentTime * 100) % 60
            );
    }

}
