using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StairBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�K�i�ɐG��܂����I");

        // ���̃t���A�����擾����
        string targetSceneName = GetNextFloorName();

        // ���̃t���A�ɐi�ށiGameManager�Ɉڏ�����j
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager script = gameManagerObject.GetComponent<GameManager>();
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
