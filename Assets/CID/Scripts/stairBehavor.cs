using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class stairBehavor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("階段に触れました！");

        // 次のフロア名を取得する
        string targetSceneName = GetNextFloorName();

        // 次のフロアに進む（GameManagerに移譲する）
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
