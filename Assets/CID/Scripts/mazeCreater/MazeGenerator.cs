using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���H�����X�N���v�g�N���X�ł��B
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    /// <summary>
    /// ���H�̕��A����
    /// </summary>
    public int width, height;

    /// <summary>
    /// ���H�����ɗp���闐�������C���X�^���X
    /// </summary>
    private readonly System.Random random = new();

    /// <summary>
    /// ���H�f�[�^
    /// </summary>
    private MazeCellModel[,] maze;

    /// <summary>
    /// ���H�̃Z����Prefab
    /// </summary>
    public GameObject mazeCellPrefab;

    /// <summary>
    /// �K�i��Prefab
    /// </summary>
    public GameObject mazeStairPrefab;

    /// <summary>
    /// ���H�𐶐������̐e�I�u�W�F�N�g
    /// </summary>
    [SerializeField] private Transform root;

    /// <summary>
    /// ���H�̊g��X�P�[��
    /// </summary>
    private float cellScale = 3f;


    /// <summary>
    /// ���H�f�[�^�����ׂď������܂��B
    /// </summary>
    public void ClearMaze()
    {
        List<GameObject> tempList = new List<GameObject>();
        foreach (Transform child in root)
        {
            tempList.Add(child.gameObject);
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            DestroyImmediate(tempList[i]);
        }
    }

    /// <summary>
    /// ���H�f�[�^�𐶐����܂��i�Ăяo�����j�B
    /// </summary>
    public void GenerateMaze()
    {
        ClearMaze();

        maze = new MazeCellModel[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = new MazeCellModel();
            }
        }
        GenerateMaze(0, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float posX = root.position.x + x * cellScale;
                float posY = 0f; //root.position.y;
                float posZ = root.position.z + y * cellScale;

                MazeCell cell = Instantiate(
                    mazeCellPrefab,
                    new Vector3(posX, posY, posZ),
                    Quaternion.identity,
                    root).GetComponent<MazeCell>();

                cell.transform.localScale *= cellScale;
                cell.name = $"{x}-{y}";

                // �����p�ɕǂ�1������菜��
                // if(x == 0 && y == 0) maze[x, y].RemoveWall(MazeCellModel.Wall.Bottom);
                // if(x == width - 1 && y == height - 1) maze[x, y].RemoveWall(MazeCellModel.Wall.Right);

                cell.Setup(maze[x, y]);
            }
        }

        // �K�i�I�u�W�F�N�g�𐶐�����ꏊ�����擾���A�����_���ɊK�i���쐬����
        List<Tuple<int, int>> stairList = new();
        for(int y = 0; y < width; y++)
        {
            for(int x = 0; x < height; x++)
            {
                // �����̍��W�͏���
                if(x == 0 && y == 0) continue;

                if(maze[x, y].IsCulDeSac())
                {
                    stairList.Add(new Tuple<int, int>(x, y));
                    Debug.Log("���F" + x + ", " + y);
                }
            }
        }

        Tuple<int, int> targetCell = stairList[new System.Random().Next(0, stairList.Count)];
        Transform rootCell = root.transform.Find("" + targetCell.Item1 + "-" + targetCell.Item2);
        Debug.Log("" + targetCell.Item1 + "-" + targetCell.Item2);

        GameObject stairObject = Instantiate(
            mazeStairPrefab,
            rootCell.position + new Vector3(0f, 0f, -1.00f),
            Quaternion.identity,
            root);
        stairObject.transform.localScale *= cellScale;

        // �K�i��z�u�����Z���̓�����ɂ���ĊK�i�̌�����ς���
        switch (maze[targetCell.Item1, targetCell.Item2].GetCulDeSacEnterDirection())
        {
            case MazeCellModel.Wall.Top:
                // -180�x��]
                stairObject.transform.Rotate(Vector3.up * 180.0f);
                stairObject.transform.position += new Vector3(0f, 0f, 2f);
                break;
            case MazeCellModel.Wall.Bottom:
                // do nothing
                break;
            case MazeCellModel.Wall.Left:
                // -90�x��]
                stairObject.transform.Rotate(Vector3.up * 90.0f);
                stairObject.transform.position += new Vector3(-1f, 0f, 1f);
                break;
            case MazeCellModel.Wall.Right:
                // -270�x��]
                stairObject.transform.Rotate(Vector3.up * 270.0f);
                stairObject.transform.position += new Vector3(1f, 0f, 1f);
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// �w�肵���T�C�Y�̖��H�f�[�^�𐶐����܂��B
    /// </summary>
    /// <param name="width">��</param>
    /// <param name="height">����</param>
    private void GenerateMaze(int width, int height)
    {
        MazeCellModel currentCell = maze[width, height];
        currentCell.visited = true;

        foreach (var direction in ShuffleDirections())
        {
            int newX = width + direction.Item1;
            int newY = height + direction.Item2;
            if (newX >= 0 && newY >= 0 && newX < this.width && newY < this.height)
            {
                MazeCellModel neighbourCell = maze[newX, newY];
                if (!neighbourCell.visited)
                {
                    neighbourCell.visited = true;
                    currentCell.RemoveWall(direction.Item3);
                    neighbourCell.RemoveWall(direction.Item4);
                    GenerateMaze(newX, newY);
                }
            }
        }
    }

    /// <summary>
    /// ���H���������̒��ŕǂ��@��i�߂�������擾���܂��B
    /// </summary>
    /// <returns>
    /// �ǂ��@��i�߂�����f�[�^
    /// </returns>
    private List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)> ShuffleDirections()
    {
        List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)> directions = new()
        {
            (0, 1, MazeCellModel.Wall.Top, MazeCellModel.Wall.Bottom),
            (0, -1, MazeCellModel.Wall.Bottom, MazeCellModel.Wall.Top),
            (-1, 0, MazeCellModel.Wall.Left, MazeCellModel.Wall.Right),
            (1, 0, MazeCellModel.Wall.Right, MazeCellModel.Wall.Left)
        };
        for (int i = 0; i < directions.Count; i++)
        {
            var temp = directions[i];
            int randomIndex = random.Next(i, directions.Count);
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }
        return directions;
    }
}