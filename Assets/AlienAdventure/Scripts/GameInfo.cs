using UnityEngine;

public class GameInfo
{
    public static float playerShieldMax = 100;
    public static float playerShield = 100;
    public static float playerBloodMax = 100;
    public static float playerBlood = 100;
    public static int playerCrystal = 0;
    public static float playerShootDistance = 100.0f;
    public static float playerHurt_Bullet = 40.0f;
    public static float playerHurt_Fire = 1.0f;
    public static LevelManager.Levels levelNow = LevelManager.Levels.LevelBase;

    public static float PlayerProportionShield
    {
        get
        {
            return playerShield / playerShieldMax;
        }
    }

    public static float PlayerProportionBlood
    {
        get
        {
            return playerBlood / playerBloodMax;
        }
    }

    public static LevelManager.Levels levelLoad = LevelManager.Levels.LevelBase;

    public static bool isPause = false;

    public static Vector3 posAimed = new Vector3();
}
