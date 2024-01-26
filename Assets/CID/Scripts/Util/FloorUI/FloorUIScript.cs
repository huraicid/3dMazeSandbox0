using UnityEngine;
using TMPro;

/// <summary>
/// �t���A���UI���X�V����X�N���v�g�N���X�ł��B
/// </summary>
public class FloorUIScript : MonoBehaviour
{
    // TextMeshPro�I�u�W�F�N�g�ւ̎Q��
    public TextMeshProUGUI textMeshPro;

    /// <summary>
    /// �t���A���UI���X�V���܂��B
    /// </summary>
    public void UpdateFloorUI()
    {
        gameObject.SetActive(true);

        // TextMeshPro�I�u�W�F�N�g���w�肳��Ă��Ȃ��ꍇ�́A�����I�Ɍ�������
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // �e�L�X�g���X�V
        UpdateText();
    }

    /// <summary>
    /// �t���A���e�L�X�g���X�V���܂��B
    /// </summary>
    void UpdateText()
    {
        // ���݂̃t���A���X�V����
        int floor = GameVariables.floor;
        textMeshPro.text = textMeshPro.text.Replace("{%floor}", floor.ToString());

        // �ŏI�K���X�V����
        int maxFloor = GameVariables.maxFloor;
        textMeshPro.text = textMeshPro.text.Replace("{%maxFloor}", maxFloor.ToString());
        Debug.Log("updated test: " + textMeshPro.text);
    }

}