using UnityEngine;

/// <summary>
/// ���H��Prefab�ɑΉ�����N���X�ł��B
/// </summary>
public class MazeCell : MonoBehaviour
{
    /// <summary>
    /// �ǂ̃��X�g�iInspector�Őݒ�\�j
    /// </summary>
    [SerializeField] private GameObject[] wallAarray = new GameObject[] { };

    /// <summary>
    /// Prefab��active�ɂ��܂��B
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

