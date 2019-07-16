

public class StoreInfo
{
    public static bool isInited = false;
    public static bool[] isShiefSolds;
    public static bool isPicked = false;

    public static void Init(int numShelfs)
    {
        isShiefSolds = new bool[numShelfs];
        for (int i = 0; i < isShiefSolds.Length; i++)
        {
            isShiefSolds[i] = false;
        }
        isPicked = false;
        isInited = true;
    }
}
