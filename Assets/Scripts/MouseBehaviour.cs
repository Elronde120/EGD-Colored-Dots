using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    public GameObject pushEffector;
    public GameObject pullEffector;

    public Camera mainCam;

    private void Start() {
        pushEffector.SetActive(false);
        pullEffector.SetActive(false);
    }
    
    private void Update() {
        MoveToMouse();
        RegisterClicks();
    }

    private void RegisterClicks(){
        if(Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift)){
            pullEffector.SetActive(true);  
            pushEffector.SetActive(false);  
        }
        else{
            pullEffector.SetActive(false);
            if(Input.GetMouseButton(1)){
                pushEffector.SetActive(true);
            }
            else{
                pushEffector.SetActive(false);
            }
        }
    }

    private void MoveToMouse(){
        if(Time.frameCount < 10) return;

        Vector3 mousePos = new Vector3(
            Input.mousePosition.x / Screen.width,
            Input.mousePosition.y / Screen.height,
            0
        );

        mousePos.x -= .5f;
        mousePos.y -= .5f;


        mousePos.y *= 2 * mainCam.orthographicSize;
        mousePos.x *= 2 * mainCam.orthographicSize * mainCam.aspect;

        transform.position = mousePos;
    }
}
