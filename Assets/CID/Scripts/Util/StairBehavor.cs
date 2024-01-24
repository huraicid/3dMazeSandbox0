using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class stairBehavor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�K�i�ɐG��܂����I");

        // ���̃t���A�����擾����
        string targetSceneName = GetNextFloorName();

        // ���̃t���A�ɐi�ށiGameManager�Ɉڏ�����j
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManagerScript script = gameManagerObject.GetComponent<GameManagerScript>();
        script.GoToNextFloor(targetSceneName);
    }

    private string GetNextFloorName()
    {
        if(GameVariables.floor == 2)
        {
            return "toyBox";
        }

        return "mazeFloor";
    }
}
