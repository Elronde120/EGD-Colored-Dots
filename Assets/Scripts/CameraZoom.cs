using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera[] cams;
    
    public void ZoomLerp(float newZoom, float time)
    {
        foreach (var cam in cams)
        {
            StartCoroutine(_ScaleLerpCor(cam, newZoom, time));
        }
        
    }

    private IEnumerator _ScaleLerpCor(Camera cam, float newZoom, float time)
    {
        float startZoom = cam.orthographicSize;
        float currentTime = 0f;
        while (Math.Abs(cam.orthographicSize - newZoom) > Mathf.Epsilon)
        {
            cam.orthographicSize = Mathf.SmoothStep(startZoom, newZoom, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
