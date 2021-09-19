using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DotSpawner : MonoBehaviour
{
	public Vector2 regionSize = Vector2.one;
	public int rejectionSamples = 30;
	public float displayRadius =1;
    public float minRadius = 0.1f;
    public float maxRadius = 1;

    [SerializeField] GameObject dotPrefab;
	List<(Vector2, float)> points;
    int index = 0;
    List<GameObject> dots;

    private void Start() {
        points = PoissonDiscSampling.GeneratePoints(minRadius, maxRadius, regionSize, rejectionSamples);
        dots = new List<GameObject>();
        DrawDots();
        transform.position -= (Vector3)regionSize / 2;
    }


    //Instantiates the random dots with their radii and gives them a random color
    void DrawDots(){
        foreach((Vector2, float) point in points){
            GameObject circle = Instantiate(dotPrefab, point.Item1, Quaternion.identity, transform);
            circle.transform.localScale = new Vector3(point.Item2, point.Item2, point.Item2);
            circle.GetComponent<SpriteRenderer>().color = new Color(Random.value,Random.value,Random.value);            
            dots.Add(circle);
        }
    }
}
