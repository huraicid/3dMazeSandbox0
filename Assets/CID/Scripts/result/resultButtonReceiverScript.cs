using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultButtonReceiverScript : MonoBehaviour
{
    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ�Ƃ��Q�[�����J�n����
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X key pressed!");

            // �J�ڐ�̃V�[����ǂݍ���
            SceneManager.LoadScene("cid_entryPoint");
        }
    }
}
