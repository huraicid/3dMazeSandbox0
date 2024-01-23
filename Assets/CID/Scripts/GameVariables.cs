using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class GameVariables
{
    // フロア情報
    public static int maxFloor = 4;
    public static int floor = 0;

    // タイマー情報
    public static bool isTimerActive = false;
    public static float currentTime = 0f;

    public static void init()
    {
        floor = 0;
        isTimerActive = false;
        currentTime = 0f;
    }
}
