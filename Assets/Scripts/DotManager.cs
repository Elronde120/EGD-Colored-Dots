using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class DotManager : MonoBehaviour
{
    public Vector2 regionSize = Vector2.one;
    public Vector2 spawnRegionSize;
    public int rejectionSamples = 30;
    public float displayRadius = 1;
    public float minRadius = 0.1f;
    public float maxRadius = 1;
    public bool generateOnStart = true;
    public bool canSpawn;

    public Slider variationSlider;

    public Camera mainCam;
    [SerializeField] GameObject dotPrefab;
    List<(Vector2, float)> points;
    int index = 0;
    public List<GameObject> dots;

    private void Start()
    {
        points = new List<(Vector2, float)>();
        points = PoissonDiscSampling.GeneratePoints(minRadius, maxRadius, regionSize, Vector2.zero, points, true,
            rejectionSamples);
        dots = new List<GameObject>();
        if (generateOnStart) GenerateDots();
    }

    private void Update()
    {
        RemoveDestroyed();
        SpawnDots();
    }

    public void CanSpawn(bool spawn)
    {
        canSpawn = spawn;
    }

    private void SpawnDots()
    {
        if (!canSpawn) return;
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            points.Clear();
            Vector3 mousePos = new Vector3(
                Input.mousePosition.x / Screen.width,
                Input.mousePosition.y / Screen.height,
                0
            );
            mousePos.x -= .5f;
            mousePos.y -= .5f;
            mousePos.y *= 2 * mainCam.orthographicSize;
            mousePos.x *= 2 * mainCam.orthographicSize * mainCam.aspect;
            for (int i = 0; i < dots.Count; i++)
            {
                points.Add((dots[i].transform.position, dots[i].transform.localScale.x));
            }

            points = PoissonDiscSampling.GeneratePoints(minRadius, maxRadius, spawnRegionSize, mousePos, points, false,
                rejectionSamples);
            GenerateDots();
        }
    }

    void RemoveDestroyed()
    {
        List<GameObject> nonDestroyed = new List<GameObject>();
        for (int i = 0; i < dots.Count; i++)
        {
            if (dots[i] != null) nonDestroyed.Add(dots[i]);
        }

        dots = nonDestroyed;
    }

    //Instantiates the random dots with their radii and gives them a random color
    public void GenerateDots(float scaleTime)
    {
        foreach ((Vector2, float) point in points)
        {
            GameObject circle = Instantiate(dotPrefab, point.Item1, Quaternion.identity, transform);
            circle.transform.localScale = new Vector3(point.Item2, point.Item2, point.Item2);
            circle.GetComponent<Dot>().ScaleLerp(new Vector3(1, 1, 1), scaleTime);

            circle.GetComponentInChildren<SpriteRenderer>().color = variationSlider
                ? ColorPallet.instance.PickColorSlightAdjustment(variationSlider.value)
                : ColorPallet.instance.PickColorSlightAdjustment(0);
            dots.Add(circle);
        }
    }

    public void HideDots(float scaleTime)
    {
        foreach (var dot in dots)
        {
            dot.GetComponent<Dot>().ScaleLerp(new Vector3(0f, 0f, 0f), scaleTime, false);
        }
    }

    public void DestroyDots(float scaleTime)
    {
        dots.Clear();
    }

    public void GenerateDots()
    {
        foreach ((Vector2, float) point in points)
        {
            GameObject circle = Instantiate(dotPrefab, point.Item1, Quaternion.identity, transform);
            circle.transform.localScale = new Vector3(point.Item2, point.Item2, point.Item2);
            circle.GetComponentInChildren<SpriteRenderer>().color = variationSlider
                ? ColorPallet.instance.PickColorSlightAdjustment(variationSlider.value)
                : ColorPallet.instance.PickColorSlightAdjustment(0);
            dots.Add(circle);
        }

        
    }

    public void ShowDots(float scaleTime)
    {
        if (dots.Count == 0)
        {
            GenerateDots();
        }

        foreach (var dot in dots)
        {
            dot.GetComponent<Dot>().ScaleLerp(new Vector3(1f, 1f, 1f), scaleTime, false);
        }
    }

    public string GetDotJSONInfo()
    {
        Dot.DotJSONInfo[] list = new Dot.DotJSONInfo[dots.Count];

        for (int i = 0; i < list.Length; i++)
        {
            
            Dot d = dots[i].GetComponent<Dot>();
            list[i] = d.ToJson();
        }
        
        return JsonHelper.ToJson(list, true);
    }

}
