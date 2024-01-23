using UnityEngine;
using UnityEngine.Events;

public class MageManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public static UnityEvent onGoal = new UnityEvent();

    private void Awake()
    {
        // �Q�[���J�n���A�p�l�����\���ɂ���
        // �܂��A���łɃC�x���g�����s����Ă���ꍇ�͂��ׂ�OFF�ɂ��Ă���
        panel.SetActive(false);
        onGoal.RemoveAllListeners();

        // �S�[���ɒ��������̃C�x���g
        onGoal.AddListener(() =>
        {
            // �p�l����\������
            panel.SetActive(true);
        });
    }
}
