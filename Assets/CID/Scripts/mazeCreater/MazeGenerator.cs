using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width, height;
    private System.Random random = new System.Random();
    private MazeCellModel[,] maze;

    public GameObject mazeCellPrefab;
    public GameObject mazeStairPrefab;
    [SerializeField] private Transform root;
    private float cellScale = 3f;


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
        List<Tuple<int, int>> stairList = new List<Tuple<int, int>>();
        for(int y = 0; y < width; y++)
        {
            for(int x = 0; x < height; x++)
            {
                // 入口の座標は除く
                if(x == 0 && y == 0) continue;

                if(maze[x, y].isCulDeSac())
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
        switch (maze[targetCell.Item1, targetCell.Item2].getCulDeSacEnterDirection())
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


    private void GenerateMaze(int x, int y)
    {
        MazeCellModel currentCell = maze[x, y];
        currentCell.visited = true;

        foreach (var direction in ShuffleDirections())
        {
            int newX = x + direction.Item1;
            int newY = y + direction.Item2;
            if (newX >= 0 && newY >= 0 && newX < width && newY < height)
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

    private List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)> ShuffleDirections()
    {
        List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)> directions = new List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)> {
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