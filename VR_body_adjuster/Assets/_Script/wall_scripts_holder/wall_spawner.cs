using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_spawner : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab; // Prefab of the wall to spawn
    [SerializeField] private Transform spawnPoint; // Spawn point for the new wall

   

    private void OnEnable()
    {
        wall_movement.OnWallDestroyed += SpawnWall;
    }

    private void OnDisable()
    {
        wall_movement.OnWallDestroyed -= SpawnWall;
    }

    private void SpawnWall()
    {
        if (wallPrefab != null && spawnPoint != null)
        {
            Instantiate(wallPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("New wall spawned!");
        }
        else
        {
            Debug.LogError("Wall prefab or spawn point is not assigned!");
        }
    }
}
