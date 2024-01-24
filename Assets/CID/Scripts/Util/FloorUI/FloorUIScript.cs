using UnityEngine;
using TMPro;

public class FloorUIScript : MonoBehaviour
{
    // TextMeshPro�I�u�W�F�N�g�ւ̎Q��
    public TextMeshProUGUI textMeshPro;

    public void updateFloorText()
    {
        gameObject.SetActive(true);
        Debug.Log("Floor UI Script ON");

        // TextMeshPro�I�u�W�F�N�g���w�肳��Ă��Ȃ��ꍇ�́A�����I�Ɍ�������
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // �����̃e�L�X�g��ݒ�
        UpdateText();
    }

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