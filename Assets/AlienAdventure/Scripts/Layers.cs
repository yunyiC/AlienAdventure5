using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers
{
    public static int Default = 1 << 0;
    public static int TransparentFX = 1 << 1;
    public static int IgnoreRaycast = 1 << 2;
    public static int Water = 1 << 4;
    public static int UI = 1 << 5;
    public static int PostProcessing = 1 << 8;

    public static int Ground = 1 << 9;
    public static int Monster = 1 << 10;
}
