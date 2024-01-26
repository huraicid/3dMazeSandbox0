using TMPro;
using UnityEngine;

/// <summary>
/// �N���A���Ԃ��X�V����X�N���v�g�N���X�ł��B
/// </summary>
public class CrearTimeUpdater : MonoBehaviour
{
    
    void Start()
    {
        // ������xx:yy.zz�\�L�ŏo�͂���
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        float currentTime = GameVariables.currentTime;
        textMeshPro.text = string.Format("{0:D2}:{1:D2}.{2:D2}",
            (int)currentTime / 60,
            (int)currentTime % 60,
            (int)(currentTime * 100) % 60
            );
    }

}
