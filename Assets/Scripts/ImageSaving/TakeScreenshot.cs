using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;


public class TakeScreenshot : MonoBehaviour {
//TODO: Account for Mobile, since it returns immediately unlike PC

    private const string C_LINUX_SAVE_LOCATION = "/../Screenshots/";
    private const string C_WINDOWS_SAVE_LOCATION = "/../Screenshots/";
    private const string C_MAC_SAVE_LOCATION = "/../Screenshots/";
    
    [SerializeField] private GameObject[] objectsToDisable;
    
    public void PCCaptureScreen(string filePath)
    {
#if !UNITY_WEBGL || UNITY_EDITOR || UNITY_STANDALONE
        
        if (OnOSX())
        {
            CheckDirectory(Application.dataPath + C_MAC_SAVE_LOCATION);
            StartCoroutine(PCCaptureScreenCor(Application.dataPath + C_MAC_SAVE_LOCATION + filePath, objectsToDisable));
        }
        else if (OnLinux())
        {
            CheckDirectory(Application.dataPath + C_LINUX_SAVE_LOCATION);
            StartCoroutine(PCCaptureScreenCor(Application.dataPath + C_LINUX_SAVE_LOCATION + filePath, objectsToDisable));
        }
        else if (OnWindows())
        {
            CheckDirectory(Application.dataPath + C_WINDOWS_SAVE_LOCATION);
            StartCoroutine(PCCaptureScreenCor(Application.dataPath + C_WINDOWS_SAVE_LOCATION + filePath, objectsToDisable));
        }

        
#endif
    }

    private void CheckDirectory(string filePath)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
    }

    private bool OnOSX()
    {
        return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
    }
    
    private bool OnLinux()
    {
        return Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxPlayer;
    }
    
    private bool OnWindows()
    {
        return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
    }
    

    public void WebGLScreenshot () {
#if !UNITY_EDITOR && UNITY_WEBGL
        StartCoroutine(UploadPNG(objectsToDisable));
#endif
    }
 
    IEnumerator UploadPNG(GameObject[] _objectsToDisable) {
        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(false);
        }
        
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();
 
        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D( width, height, TextureFormat.RGB24, false );
 
        // Read screen contents into the texture
        tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
        tex.Apply();
 
        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Destroy( tex );
 
        //string ToBase64String byte[]
        string encodedText = System.Convert.ToBase64String (bytes);
   
        var image_url = "data:image/png;base64," + encodedText;
 
        Debug.Log (image_url);
 
        
        SaveScreenshotWebGL("test file", image_url);
        
        
        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(true);
        }
    }
    
    
    private IEnumerator PCCaptureScreenCor(string _filePath, GameObject[] _objectsToDisable)
    {
        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(false);
        }
        
        
        //TODO: Account for other operating systems file formats
        yield return new WaitForFixedUpdate();
        ScreenCapture.CaptureScreenshot(_filePath);
        yield return new WaitForFixedUpdate();
        
        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(true);
        }
    }
    
 
    [DllImport("__Internal")]
    private static extern void SaveScreenshotWebGL(string filename, string data);
 
}     