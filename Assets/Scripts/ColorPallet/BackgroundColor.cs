using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    public Camera cam;

    void Start()
    {

    }

    public void RandomizeBackground()
    {
        cam.backgroundColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void WhiteBackground()
    {
        cam.backgroundColor = new Color(1f, 1f, 1f);
    }

    public void BlackBackground()
    {
        cam.backgroundColor = new Color(0f, 0f, 0f);
    }
}
