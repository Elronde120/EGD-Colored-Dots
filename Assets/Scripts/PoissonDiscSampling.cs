using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling{

    //Returns a list of all the positions of the dots and their radii
    public static List<(Vector2, float)> GeneratePoints(float minRadius, float maxRadius, Vector2 region, Vector2 center, List<(Vector2, float)> points, bool initialSpawn, int numSamplesBeforeRejection = 30){
        float cellSize = 0.1f;
        int[,] grid = new int[Mathf.CeilToInt(region.x/cellSize),Mathf.CeilToInt(region.y/cellSize)];
        List<(Vector2, float)> spawnPoints = new List<(Vector2, float)>();
        List<(Vector2, float)> newPoints = new List<(Vector2, float)>();
        for(int i = 0; i < points.Count; i++){
            Vector2 adjustedPos = points[i].Item1 - center + region / 2;
            if(adjustedPos.x > 0 && adjustedPos.x < region.x && adjustedPos.y > 0 && adjustedPos.y < region.y){
                grid[(int)(adjustedPos.x / cellSize),(int)(adjustedPos.y / cellSize)] = i;
                spawnPoints.Add((adjustedPos, points[i].Item2));
            }
        }
        if(spawnPoints.Count == 0)
            spawnPoints.Add((Vector2.zero, Random.Range(minRadius, maxRadius)));
        while(spawnPoints.Count > 0){
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnPoint = spawnPoints[spawnIndex].Item1;
            float spawnRadius = spawnPoints[spawnIndex].Item2;
            bool accepted = false;
            float radius = Random.Range(minRadius, maxRadius);
            for(int i = 0; i < numSamplesBeforeRejection; i++){
                float angle = Random.value * 2 * Mathf.PI;
                Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
                Vector2 candidate = spawnPoint + dir * Random.Range(spawnRadius + radius, spawnRadius + 2 * radius);
                if(isValid(candidate, region, cellSize, radius, center, points, grid, initialSpawn)){
                    points.Add((candidate - region / 2 + center, radius));
                    newPoints.Add((candidate - region / 2 + center, radius));
                    spawnPoints.Add((candidate, radius));
                    grid[(int)(candidate.x / cellSize),(int)(candidate.y / cellSize)] = points.Count;
                    accepted = true;
                    break;
                }
            }
            if(!accepted) spawnPoints.RemoveAt(spawnIndex);
        }
        return newPoints;
    }

    //Checks if there's any dot within this dot's radius box
    public static bool isValid(Vector2 candidate, Vector2 region, float cellSize, float radius, Vector2 center, List<(Vector2,float)> points, int[,] grid, bool initialSpawn){
        if(candidate.x >= 0 && candidate.x < region.x && candidate.y >= 0 && candidate.y < region.y){
            int cellRange = 20;
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int startCellX = Mathf.Max(0, cellX - cellRange);
            int endCellX = Mathf.Min(cellX + cellRange, grid.GetLength(0) - 1);
            int startCellY = Mathf.Max(0, cellY - cellRange);
            int endCellY = Mathf.Min(cellY + cellRange, grid.GetLength(1) - 1);
            for(int x = startCellX; x <= endCellX; x++){
                for(int y = startCellY; y <= endCellY; y++){
                    int pointIndex = grid[x,y] - 1;
                    if(pointIndex != -1){
                        float distance = Vector2.Distance(candidate, points[pointIndex].Item1 + region / 2 - center);
                        //The initial spawn spawns a lot more dots
                        if(initialSpawn){
                            //This is for more dots but they have to move apart at the beginning
                            if(distance < radius){
                                return false;
                            }
                        }
                        //The manual spawn only spawns if there is space
                        else{
                            if(distance < radius + points[pointIndex].Item2){
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}
