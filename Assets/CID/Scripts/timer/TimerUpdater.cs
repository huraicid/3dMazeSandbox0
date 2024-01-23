using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUpdater : MonoBehaviour
{
    // TextMeshProオブジェクトへの参照
    private TextMeshProUGUI textMeshPro = null;

    private void Start()
    {
        // TextMeshProオブジェクトが指定されていない場合は、自動的に検索する
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(textMeshPro == null) 
        {
            return;
        }

        float currentTime = GameVariables.currentTime;
        textMeshPro.text = string.Format("{0:D2}:{1:D2}.{2:D2}",
            (int) currentTime / 60,
            (int) currentTime % 60,
            (int) (currentTime * 100) % 60
            );
    }
}
