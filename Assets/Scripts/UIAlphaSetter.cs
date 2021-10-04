using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UIAlphaSetter : MonoBehaviour
{
    public float startAlpha = 1f;
    public bool startInteractable = true;
    private Graphic[] _graphics;
    // Start is called before the first frame update
    void Start()
    {
        _graphics = GetComponentsInChildren<Graphic>();
        LerpAlpha(startAlpha, 0.1f, startInteractable);

    }

    public void LerpAlpha(float newAlpha, float time, bool interactableAtEndOfLerp = true)
    {
        foreach (var graphic in _graphics)
        {
            StartCoroutine(_ScaleLerpCor(graphic, newAlpha, time, interactableAtEndOfLerp));
        }
    }
    
    private IEnumerator _ScaleLerpCor(Graphic graphic, float newAlpha, float time, bool interactableAtEndOfLerp = true)
    {
        graphic.raycastTarget = false;
        float colorA = graphic.color.a;
        float currentTime = 0f;
        while (Mathf.Abs(graphic.color.a - newAlpha) > Mathf.Epsilon)
        {
            float alpha = Mathf.SmoothStep(colorA, newAlpha, currentTime / time);
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        graphic.raycastTarget = interactableAtEndOfLerp;
    }
}
