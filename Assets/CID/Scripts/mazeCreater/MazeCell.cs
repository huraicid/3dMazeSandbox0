using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 迷路生成処理に使う迷路データクラスです。
/// このクラスは迷路のPrefabではなく、抽象的なデータのみに使用されます。
/// </summary>
public class MazeCellModel
{
    /// <summary>
    /// 壁の方向定義
    /// </summary>
    public enum Wall { Top, Bottom, Left, Right }

    /// <summary>
    /// 迷路を生成するときに訪れたことがあるかどうか
    /// </summary>
    public bool visited = false;

    /// <summary>
    /// 壁の方向と壁があるかどうかの対応リスト
    /// </summary>
    private Dictionary<Wall, bool> walls = new()
    {
        { Wall.Top, true },
        { Wall.Bottom, true },
        { Wall.Left, true },
        { Wall.Right, true }
    };

    /// <summary>
    /// 引数の方向の壁を除去します。
    /// </summary>
    /// <param name="wall">対象の壁（Wall型）</param>
    public void RemoveWall(Wall wall)
    {
        walls[wall] = false;
    }

    /// <summary>
    /// 引数の方向の壁が存在するか判定します。
    /// </summary>
    /// <param name="wall">対象の壁（Wall型）</param>
    /// <returns>
    /// 引数の方向の壁が存在するかどうか
    /// </returns>
    public bool HasWall(Wall wall)
    {
        return walls[wall];
    }

    /// <summary>
    /// セルが袋小路であるか判定します
    /// </summary>
    /// <returns>
    /// 袋小路であるかどうか
    /// </returns>
    /// <remarks>
    /// 袋小路とは、4方向の壁候補のうち、3つが壁になっていて、
    /// 残りの1方向は壁がないセルをいいます。
    /// </remarks>
    public bool IsCulDeSac()
    {
        int wallCount = 0;
        foreach (Wall wall in Enum.GetValues(typeof(Wall)))
        {
            if (!walls[wall]) { wallCount++; }
        }

        if(wallCount == 1) { return true; }

        return false;
    }

    /// <summary>
    /// 袋小路の入り口（壁がない方向）を取得します。
    /// </summary>
    /// <returns>
    /// 袋小路の入り口（壁がない方向）（Wall型）
    /// </returns>
    public Wall GetCulDeSacEnterDirection()
    {
        foreach(Wall wall in Enum.GetValues(typeof(Wall))) {
            if (!walls[wall])
            {
                return wall;
            }
        }

        return Wall.Top;
    }
}

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

