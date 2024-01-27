using UnityEngine;

/// <summary>
/// 迷路のPrefabに対応するクラスです。
/// </summary>
public class MazeCell : MonoBehaviour
{
    /// <summary>
    /// 壁のリスト（Inspectorで設定可能）
    /// </summary>
    [SerializeField] private GameObject[] wallAarray = new GameObject[] { };

    /// <summary>
    /// Prefabをactiveにします。
    /// </summary>
    /// <param name="mazeCellModel"></param>
    public void Setup(MazeCellModel mazeCellModel)
    {
        wallAarray[(int)MazeCellModel.Wall.Top].SetActive(mazeCellModel.HasWall(MazeCellModel.Wall.Top));

        for (int i = 0; i < (int)MazeCellModel.Wall.Right + 1; i++)
        {
            wallAarray[i].SetActive(mazeCellModel.HasWall((MazeCellModel.Wall)i));
        }
    }
}

