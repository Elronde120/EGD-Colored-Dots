using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    public GameObject pushEffector;
    public GameObject pullEffector;

    public GameObject pullAllEffector;

    public GameObject coloredPushEffector;

    public DotSpawner dotSpawner;

    public Camera mainCam;

    public LayerMask raycastLayerMask;
    
    public LineRenderer lineRenderer;

    public float pushPullRadius;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        pushEffector.SetActive(false);
        pullEffector.SetActive(false);
        coloredPushEffector.SetActive(false);
        pullAllEffector.SetActive(false);
        StartCoroutine("ChangeDotsOfColorToLayer");
    }
    
    private void Update() {
        MoveToMouse();
        RegisterClicks();
        pushEffector.GetComponent<CircleCollider2D>().radius = pushPullRadius;
        pullEffector.GetComponent<CircleCollider2D>().radius = pushPullRadius;
        pullAllEffector.GetComponent<CircleCollider2D>().radius = pushPullRadius;
        coloredPushEffector.GetComponent<CircleCollider2D>().radius = pushPullRadius;
        dotSpawner.GetComponent<DotSpawner>().spawnRegionSize = new Vector2(pushPullRadius * 2, pushPullRadius * 2);
        pushPullRadius += Input.mouseScrollDelta.y * 0.1f;
        if(pushPullRadius < 0.2f)
        {
            pushPullRadius = 0.2f;
        }
        if(pushPullRadius > 2f)
        {
            pushPullRadius = 2f;
        }
        Circle(pushPullRadius, 0.1f);
    }

    int dotsIndex = 0;
    Color currentColor;
    private IEnumerator ChangeDotsOfColorToLayer(){
        while(true){
            yield return new WaitForEndOfFrame();
            for(int i = 0; i < 20; i++){
                if(dotSpawner.dots == null || dotSpawner.dots.Count == 0) continue;
                GameObject g;
                try{
                    g = dotSpawner.dots[dotsIndex];
                }
                catch(System.Exception){
                    dotsIndex = 0;
                    g = dotSpawner.dots[dotsIndex];
                }
                dotsIndex++;
                if(AreColorsSimilar(g.GetComponentInChildren<SpriteRenderer>().color, currentColor)){
                    g.layer = 6;
                }
                else{
                    g.layer = 0;
                }
            }
        }
    }

    private void RegisterClicks(){
        if(Input.GetMouseButtonUp(0)){
            currentColor = new Color(0,0,0,0);
            pullEffector.SetActive(false);
            coloredPushEffector.SetActive(false);
            pullAllEffector.SetActive(false);
        }
        if(Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift)){
            pushEffector.SetActive(false);
            if(Input.GetMouseButtonDown(0)){
                    //the first frame we click with the right mouse button
                    //find all nearby dots and set their layers to the new layer
                    var clickedDot = Physics2D.Raycast(transform.position, Vector2.one, .01f, raycastLayerMask);
                    if(clickedDot.collider == null){
                        //if we didn't click a dot
                        pullAllEffector.SetActive(true);
                    }
                    else{
                        pullEffector.SetActive(true);
                        coloredPushEffector.SetActive(true);

                        //if we clicked a dot, choose that one to pull colors by
                        var color = clickedDot.collider.GetComponentInChildren<SpriteRenderer>().color;
                        currentColor = color;
                        var sphereCast = Physics2D.OverlapCircleAll(transform.position, 5);
                        foreach(var item in sphereCast){
                            if(item.gameObject.layer == 3 || item.gameObject.layer == 5) continue;
                            if(AreColorsSimilar(item.GetComponentInChildren<SpriteRenderer>().color, color)){
                                //turn the layer to the correct one
                                item.gameObject.layer = 6;
                            }
                            else{
                                item.gameObject.layer = 0;
                            }
                        }
                    }
                    
                }
            
        }
        else{
            if(Input.GetMouseButton(1)){
                pushEffector.SetActive(true);
            }
            else{
                pullEffector.SetActive(false);
                pushEffector.SetActive(false);
                pullAllEffector.SetActive(false);
                coloredPushEffector.SetActive(false);
            }
        }
    }

    public float colorDiscrepency = .2f;
    private bool AreColorsSimilar(Color a, Color b){
        if(Mathf.Abs(a.r - b.r) < colorDiscrepency && 
           Mathf.Abs(a.g - b.g) < colorDiscrepency &&
           Mathf.Abs(a.b - b.b) < colorDiscrepency){
               return true;
           }
        return false;
    }

    private void MoveToMouse()
    {
        if (Time.frameCount < 10) return;

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

    public void Circle(float radius, float lineWidth)
    {
        var segments = 360;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        lineRenderer.SetPositions(points);
    }
}
