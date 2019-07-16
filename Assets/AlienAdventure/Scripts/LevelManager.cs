using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    public enum Levels : int
    {
        LevelMainMenu = 0,
        Level0 = 1,
        Level1 = 2,
        Level2 = 3,
        Level3 = 4,
        LevelBase = 5,
        LevelStore = 6,
        LevelTraining = 7,
        LevelLoad = 8,
        LevelEnd = 9,
        LevelTestPortalA,
        LevelTestPortalB,

    }

    public static string[] LevelNameE = new string[] {
        "LevelMainMenu",
        "Level0",
        "Level1",
        "Level2",
        "Level3",
        "LevelBase",
        "LevelStore",
        "LevelTraining",
        "LevelLoad",
        "LevelEnd",
        "Test_PortalA",
        "Test_PortalB",
    };
    public static string[] LevelNameC = new string[] {
        "主菜单",
        "第零关",
        "第一关",
        "第二关",
        "第三关",
        "基地",
        "商店",
        "训练场",
        "加载中...",
        "游戏结束",
        "传送测试A",
        "传送测试B",

    };

    public static string GetLevelNameE(Levels level)
    {
        return LevelNameE[(int)level];
    }

    public static string GetLevelNameC(Levels level)
    {
        return LevelNameC[(int)level];
    }

    public static int GetLevelNum(Levels level)
    {
        return (int)level;
    }

    public static void LoadLevel(Levels newLevel)
    {
        GameInfo.levelLoad = newLevel;
        SceneManager.LoadScene(GetLevelNameE(Levels.LevelLoad));
    }
}
