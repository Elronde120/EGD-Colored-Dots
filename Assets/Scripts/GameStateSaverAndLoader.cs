using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSaverAndLoader : MonoBehaviour
{
    [SerializeField] private DotManager manager;

    private const string C_LINUX_SAVE_LOCATION = "/../Saves/";
    private const string C_WINDOWS_SAVE_LOCATION = "/../Saves/";
    private const string C_MAC_SAVE_LOCATION = "/../Saves/";
    private const string C_FILE_EXTENSION_TXT = ".txt";
    private const string C_FILE_NAME = "save";
    
    // TODO: Account for Mobile and WebGL
    public void SaveDotData()
    {
        string jsonData = manager.GetDotJSONInfo();

#if !UNITY_WEBGL || UNITY_EDITOR || UNITY_STANDALONE

        string savePath = "";
        string fullSavePath = "";

        if (OSDeterminator.OnOSX())
        {
            savePath = Application.dataPath + C_MAC_SAVE_LOCATION;
            fullSavePath = Application.dataPath + C_MAC_SAVE_LOCATION + C_FILE_NAME + C_FILE_EXTENSION_TXT;
        }
        else if (OSDeterminator.OnLinux())
        {
            savePath = Application.dataPath + C_LINUX_SAVE_LOCATION;
            fullSavePath = Application.dataPath + C_LINUX_SAVE_LOCATION + C_FILE_NAME +  C_FILE_EXTENSION_TXT;
        }
        else if (OSDeterminator.OnWindows())
        {
            savePath = Application.dataPath + C_WINDOWS_SAVE_LOCATION;
            fullSavePath = Application.dataPath + C_WINDOWS_SAVE_LOCATION + C_FILE_NAME + C_FILE_EXTENSION_TXT;
        }

        JSONFileIO.CheckDirectory(savePath);
        JSONFileIO.SaveToFile(fullSavePath, jsonData);
#endif
    }

    public void TestLoadState()
    {
        LoadState();
    }

    public Dot.DotJSONInfo[] LoadState()
    {
#if !UNITY_WEBGL || UNITY_EDITOR || UNITY_STANDALONE

        string savePath = "";
        string fullSavePath = "";

        if (OSDeterminator.OnOSX())
        {
            savePath = Application.dataPath + C_MAC_SAVE_LOCATION;
            fullSavePath = Application.dataPath + C_MAC_SAVE_LOCATION + C_FILE_NAME + C_FILE_EXTENSION_TXT;
        }
        else if (OSDeterminator.OnLinux())
        {
            savePath = Application.dataPath + C_LINUX_SAVE_LOCATION;
            fullSavePath = Application.dataPath + C_LINUX_SAVE_LOCATION + C_FILE_NAME + C_FILE_EXTENSION_TXT;
        }
        else if (OSDeterminator.OnWindows())
        {
            savePath = Application.dataPath + C_WINDOWS_SAVE_LOCATION;
            fullSavePath = Application.dataPath + C_WINDOWS_SAVE_LOCATION + C_FILE_NAME + C_FILE_EXTENSION_TXT;
        }

        string fileData = JSONFileIO.ReadFile(fullSavePath);

        if(string.IsNullOrWhiteSpace(fileData))
        {
            Debug.LogError($"Failed to read any data from save path: \"{fullSavePath}\"");
            return null;
        }

        Dot.DotJSONInfo[] result = JsonHelper.FromJson<Dot.DotJSONInfo>(fileData);
        return result;

#endif
        return null;
    }


}
