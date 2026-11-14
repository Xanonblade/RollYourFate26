using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    private List<Transform> spawnPoints;

    public List<Transform> SpawnPointsList => spawnPoints;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>().ToList();
        spawnPoints.Remove(transform);
        // Debug.Log(spawnPoints.Count);
    }

}
