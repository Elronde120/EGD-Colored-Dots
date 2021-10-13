using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSaver : MonoBehaviour
{
    [SerializeField] private DotManager manager;

    private const string C_LINUX_SAVE_LOCATION = "/../Saves/";
    private const string C_WINDOWS_SAVE_LOCATION = "/../Saves/";
    private const string C_MAC_SAVE_LOCATION = "/../Saves/";
    private const string C_FILE_EXTENSION_TXT = ".txt";
    
    public void SaveDotData()
    {
        string jsonData = manager.GetDotJSONInfo();
        
#if !UNITY_WEBGL || UNITY_EDITOR || UNITY_STANDALONE
        
        if (OSDeterminator.OnOSX())
        {
            JSONFileIO.CheckDirectory(Application.dataPath + C_MAC_SAVE_LOCATION);
            JSONFileIO.SaveToFile(Application.dataPath + C_MAC_SAVE_LOCATION + C_FILE_EXTENSION_TXT, jsonData);
        }
        else if (OSDeterminator.OnLinux())
        {
            JSONFileIO.CheckDirectory(Application.dataPath + C_LINUX_SAVE_LOCATION);
            JSONFileIO.SaveToFile(Application.dataPath + C_LINUX_SAVE_LOCATION + C_FILE_EXTENSION_TXT, jsonData);
        }
        else if (OSDeterminator.OnWindows())
        {
            JSONFileIO.CheckDirectory(Application.dataPath + C_WINDOWS_SAVE_LOCATION);
            JSONFileIO.SaveToFile(Application.dataPath + C_WINDOWS_SAVE_LOCATION + C_FILE_EXTENSION_TXT, jsonData);
        }

        
#endif
    
        
    }
    
    
}
