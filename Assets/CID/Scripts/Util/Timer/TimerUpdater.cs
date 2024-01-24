using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUpdater : MonoBehaviour
{
    // TextMeshPro�I�u�W�F�N�g�ւ̎Q��
    private TextMeshProUGUI textMeshPro = null;

    private void Start()
    {
        // TextMeshPro�I�u�W�F�N�g���w�肳��Ă��Ȃ��ꍇ�́A�����I�Ɍ�������
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
