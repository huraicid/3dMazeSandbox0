using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 迷路生成スクリプトクラスです。
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    /// <summary>
    /// 迷路の幅、高さ
    /// </summary>
    public int width, height;

    /// <summary>
    /// 迷路生成に用いる乱数生成インスタンス
    /// </summary>
    private readonly System.Random random = new();

    /// <summary>
    /// 迷路データ
    /// </summary>
    private MazeCellModel[,] maze;

    /// <summary>
    /// 迷路のセルのPrefab
    /// </summary>
    public GameObject mazeCellPrefab;

    /// <summary>
    /// 階段のPrefab
    /// </summary>
    public GameObject mazeStairPrefab;

    /// <summary>
    /// 迷路を生成する先の親オブジェクト
    /// </summary>
    [SerializeField] private Transform root;

    /// <summary>
    /// 迷路の拡大スケール
    /// </summary>
    private float cellScale = 3f;


    /// <summary>
    /// 迷路データをすべて消去します。
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
    /// 迷路データを生成します（呼び出し元）。
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

                // 入口用に壁を1か所取り除く
                // if(x == 0 && y == 0) maze[x, y].RemoveWall(MazeCellModel.Wall.Bottom);
                // if(x == width - 1 && y == height - 1) maze[x, y].RemoveWall(MazeCellModel.Wall.Right);

                cell.Setup(maze[x, y]);
            }
        }

        // 階段オブジェクトを生成する場所候補を取得し、ランダムに階段を作成する
        List<Tuple<int, int>> stairList = new();
        for(int y = 0; y < width; y++)
        {
            for(int x = 0; x < height; x++)
            {
                // 入口の座標は除く
                if(x == 0 && y == 0) continue;

                if(maze[x, y].IsCulDeSac())
                {
                    stairList.Add(new Tuple<int, int>(x, y));
                    Debug.Log("候補：" + x + ", " + y);
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

        // 階段を配置したセルの入り口によって階段の向きを変える
        switch (maze[targetCell.Item1, targetCell.Item2].GetCulDeSacEnterDirection())
        {
            case MazeCellModel.Wall.Top:
                // -180度回転
                stairObject.transform.Rotate(Vector3.up * 180.0f);
                stairObject.transform.position += new Vector3(0f, 0f, 2f);
                break;
            case MazeCellModel.Wall.Bottom:
                // do nothing
                break;
            case MazeCellModel.Wall.Left:
                // -90度回転
                stairObject.transform.Rotate(Vector3.up * 90.0f);
                stairObject.transform.position += new Vector3(-1f, 0f, 1f);
                break;
            case MazeCellModel.Wall.Right:
                // -270度回転
                stairObject.transform.Rotate(Vector3.up * 270.0f);
                stairObject.transform.position += new Vector3(1f, 0f, 1f);
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// 指定したサイズの迷路データを生成します。
    /// </summary>
    /// <param name="width">幅</param>
    /// <param name="height">高さ</param>
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
    /// 迷路生成処理の中で壁を掘り進める方向を取得します。
    /// </summary>
    /// <returns>
    /// 壁を掘り進める方向データ
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