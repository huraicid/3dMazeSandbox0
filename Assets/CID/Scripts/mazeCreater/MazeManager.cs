using UnityEngine;
using UnityEngine.Events;

public class MageManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public static UnityEvent onGoal = new UnityEvent();

    private void Awake()
    {
        // ゲーム開始時、パネルを非表示にする
        // また、すでにイベントが実行されている場合はすべてOFFにしておく
        panel.SetActive(false);
        onGoal.RemoveAllListeners();

        // ゴールに着いた時のイベント
        onGoal.AddListener(() =>
        {
            // パネルを表示する
            panel.SetActive(true);
        });
    }
}
