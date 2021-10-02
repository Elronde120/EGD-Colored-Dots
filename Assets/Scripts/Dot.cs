using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleLerp(Vector3 newScale, float time)
    {
        StartCoroutine(_ScaleLerpCor(newScale, time));
    }

    private IEnumerator _ScaleLerpCor(Vector3 newScale, float time)
    {
        Vector3 startScale = spriteTransform.localScale;
        float currentTime = 0f;
        while (spriteTransform.localScale != newScale)
        {
            float x = Mathf.Lerp(startScale.x, newScale.x, currentTime / time);
            float y = Mathf.Lerp(startScale.x, newScale.x, currentTime / time);
            float z = Mathf.Lerp(startScale.x, newScale.x, currentTime / time);
            spriteTransform.localScale = new Vector3(x, y, z);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
