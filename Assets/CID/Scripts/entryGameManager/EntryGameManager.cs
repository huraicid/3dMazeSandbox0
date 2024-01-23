using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryGameManager : MonoBehaviour
{
    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ�Ƃ��Q�[�����J�n����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed!");

            // �e��f�[�^������������
            GameVariables.init();

            // �J�ڐ�̃V�[����ǂݍ���
            SceneManager.LoadScene("mazeFloor");
        }

        // q�L�[�������ꂽ�Ƃ��Q�[�����I������
        if(Input.GetKeyDown(KeyCode.Q)) {
            QuitGame();
        }
    }

    /// <summary>
    /// �Q�[�����I�����܂��B
    /// </summary>
    /// <remarks>
    /// Unity Editor�����ۂ̎��s���ނ��ŃQ�[�����I�����鏈�����Ⴄ���߁A
    /// �r���h���ɂǂ̂悤�ȏ��������{���邩�A�����̂悤�Ɏw�肷��K�v������܂��B
    /// �i�C���f���g���܂߂ďd�v�ł��j�j
    /// </remarks>
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }
}
