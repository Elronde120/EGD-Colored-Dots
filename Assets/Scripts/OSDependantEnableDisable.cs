using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSDependantEnableDisable : MonoBehaviour
{

    [SerializeField] private ObjectsToDisableEnable enable_disable_windows;
    [SerializeField] private ObjectsToDisableEnable enable_disable_linux;
    [SerializeField] private ObjectsToDisableEnable enable_disable_osx;
    [SerializeField] private ObjectsToDisableEnable enable_disable_webGL;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (OSDeterminator.OnWindows())
        {
            PreformLogic(enable_disable_windows);
        }
        else if (OSDeterminator.OnLinux())
        {
            PreformLogic(enable_disable_linux);
        }
        else if (OSDeterminator.OnOSX())
        {
            PreformLogic(enable_disable_osx);
        }
        else
        {
            //must be on webGL
            PreformLogic(enable_disable_webGL);
        }
    }

    private static void PreformLogic(ObjectsToDisableEnable objects)
    {
        foreach (var obj in objects.objectsToEnable)
        {
            if(obj)
                obj.SetActive(true);
        }
        
        foreach (var obj in objects.objectsToDisable)
        {
            if(obj)
                obj.SetActive(false);
        }
    }


    [System.Serializable]
    private struct ObjectsToDisableEnable
    {
        public GameObject[] objectsToEnable;
        public GameObject[] objectsToDisable;
    }
}


