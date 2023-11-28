using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static int Kills { get; set; }
    public static int Deaths { get; set; }
    public static int Assists { get; set; }

    public static bool called = false;
    public static float volume { get; set; }
    public static float music { get; set; }
    public static float jumpForce { get; set; }
    public static bool enableBar { get; set; }
    public static bool DontPlayEntryMusic { get; set; }

    public static void initializeFirst()
    {
        if(!called)
        {
            DontPlayEntryMusic = true;
            volume = 0f;
            music = 0f;
            jumpForce = 9f;
            called = true;
            enableBar = false;
        }
    }
}
