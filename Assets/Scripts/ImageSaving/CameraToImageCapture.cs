using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToImageCapture : MonoBehaviour
{
    public void CaptureScreen(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
    }
}
