using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OSDeterminator
{
    public static bool OnOSX()
    {
        return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
    }
    
    public static bool OnLinux()
    {
        return Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxPlayer;
    }
    
    public static bool OnWindows()
    {
        return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
    }
}
