using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToImageCapture : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDisable;
    public void CaptureScreen(string filePath)
    {
        StartCoroutine(CaptureScreenCor(filePath, objectsToDisable));
    }

    private IEnumerator CaptureScreenCor(string _filePath, GameObject[] _objectsToDisable)
    {
        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(false);
        }
        
        //TODO: Account for Mobile, since it returns immediatly unlike PC
        yield return new WaitForFixedUpdate();
        ScreenCapture.CaptureScreenshot(_filePath);
        yield return new WaitForFixedUpdate();
        
        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(true);
        }
    }
}
