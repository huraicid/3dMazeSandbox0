using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �t���A�����[�h����Ƃ��̏���
    void Start()
    {
        if(!GameVariables.isTimerActive)
        {
            GameVariables.isTimerActive = true;
        }

        // �C�x���g�ɃC�x���g�n���h���[��ǉ�
        SceneManager.sceneLoaded += SceneLoaded;
        Debug.Log("�ǂݍ���OK");

        if (GameVariables.floor < 1)
        {
            GameVariables.floor = 1;
        }

        // ���H�t���A�Ȃ���H�𐶐�
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene Name: " + currentSceneName);
        if (currentSceneName == "mazeFloor")
        {
            GameObject gameManagerObject = GameObject.Find("MazeManager");
            MazeGenerator script = gameManagerObject.GetComponent<MazeGenerator>();
            script.GenerateMaze();
        }

        // Floor����UI���X�V
        updateFloorUI();

    }

    // �}�C�t���[�����s����鏈��
    private void Update()
    {
        // game abort
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("cid_entryPoint");
        }
        
        // �Q�[���o�ߎ��Ԃ��v�Z
        if (GameVariables.isTimerActive)
        {
            GameVariables.currentTime += Time.deltaTime;
        }
    }

    // ���̃t���A�ɐi�ނƂ��̏���
    public void GoToNextFloor(string nextFloorName)
    {
        Debug.Log("isGoal:" + (GameVariables.maxFloor == GameVariables.floor));
        if(GameVariables.maxFloor == GameVariables.floor)
        {
            GameVariables.isTimerActive = false;
            SceneManager.LoadScene("result");
            return;
        }

        Debug.Log("GoToNextFloor()���Ăяo����܂����Bnext: " + nextFloorName);

        // �t�F�[�h�A�E�g���J�n����
        FadeOut();

        // �K����1���₷
        GameVariables.floor++;

        // �J�ڐ�̃V�[����ǂݍ���
        Debug.Log("next floor: " +  nextFloorName);
        SceneManager.LoadScene(nextFloorName);
    }

    // �t�F�[�h�A�E�g����
    private void FadeOut()
    {
        Image fadeImage = null;      // �t�F�[�h�p��UI�p�l���iImage�j
        float fadeDuration = 10.0f;   // �t�F�[�h�̊����ɂ����鎞��

        Debug.Log("FadeOut()���Ăяo����܂���");

        GameObject foundObject = GameObject.Find("Image");
        // �擾����GameObject��null�łȂ����Ƃ��m�F
        if (foundObject == null)
        {
            UnityEngine.Debug.LogWarning("�w�肳�ꂽ���O��GameObject��������܂���ł����B");
            return;
        }
        Debug.Log("�t�F�[�h�A�E�g�p�̃Q�[���I�u�W�F�N�gOK");

        // Image�R���|�[�l���g���擾
        Image imageComponent = foundObject.GetComponent<Image>();
        if (imageComponent == null)
        {
            UnityEngine.Debug.LogWarning("GameObject��Image�R���|�[�l���g���A�^�b�`����Ă��܂���B");
            return;
        }
        Debug.Log("�t�F�[�h�A�E�g�p��Image�R���|�[�l���gOK");

        // �擾����Image�R���|�[�l���g���g�p����
        fadeImage = imageComponent;

        // �t�F�[�h�A�E�g�̃A�j���[�V����
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, timer / fadeDuration);
            return;
        }
    }

    // �t���A�����擾����
    private static void updateFloorUI()
    {
        GameObject floorTextObject = GameObject.Find("FloorText");
        if (floorTextObject == null)
        {
            Debug.LogWarning("floorText���擾�ł��܂���ł���");
            return;
        }
        Debug.Log("floor Text�擾�ł��܂���");

        //
        FloorUIScript script = floorTextObject.GetComponent<FloorUIScript>();
        if (script == null)
        {
            Debug.LogWarning("floorUIScript���擾�ł��܂���ł���");
            return;
        }

        script.updateFloorText();
    }

    // �C�x���g�n���h���[�i�C�x���g�������ɓ��������������j
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log(nextScene.name);
        Debug.Log(mode);
    }
}