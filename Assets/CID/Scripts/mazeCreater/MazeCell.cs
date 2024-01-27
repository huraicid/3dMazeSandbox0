using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���H���������Ɏg�����H�f�[�^�N���X�ł��B
/// ���̃N���X�͖��H��Prefab�ł͂Ȃ��A���ۓI�ȃf�[�^�݂̂Ɏg�p����܂��B
/// </summary>
public class MazeCellModel
{
    /// <summary>
    /// �ǂ̕�����`
    /// </summary>
    public enum Wall { Top, Bottom, Left, Right }

    /// <summary>
    /// ���H�𐶐�����Ƃ��ɖK�ꂽ���Ƃ����邩�ǂ���
    /// </summary>
    public bool visited = false;

    /// <summary>
    /// �ǂ̕����ƕǂ����邩�ǂ����̑Ή����X�g
    /// </summary>
    private Dictionary<Wall, bool> walls = new()
    {
        { Wall.Top, true },
        { Wall.Bottom, true },
        { Wall.Left, true },
        { Wall.Right, true }
    };

    /// <summary>
    /// �����̕����̕ǂ��������܂��B
    /// </summary>
    /// <param name="wall">�Ώۂ̕ǁiWall�^�j</param>
    public void RemoveWall(Wall wall)
    {
        walls[wall] = false;
    }

    /// <summary>
    /// �����̕����̕ǂ����݂��邩���肵�܂��B
    /// </summary>
    /// <param name="wall">�Ώۂ̕ǁiWall�^�j</param>
    /// <returns>
    /// �����̕����̕ǂ����݂��邩�ǂ���
    /// </returns>
    public bool HasWall(Wall wall)
    {
        return walls[wall];
    }

    /// <summary>
    /// �Z�����܏��H�ł��邩���肵�܂�
    /// </summary>
    /// <returns>
    /// �܏��H�ł��邩�ǂ���
    /// </returns>
    /// <remarks>
    /// �܏��H�Ƃ́A4�����̕ǌ��̂����A3���ǂɂȂ��Ă��āA
    /// �c���1�����͕ǂ��Ȃ��Z���������܂��B
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
    /// �܏��H�̓�����i�ǂ��Ȃ������j���擾���܂��B
    /// </summary>
    /// <returns>
    /// �܏��H�̓�����i�ǂ��Ȃ������j�iWall�^�j
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

