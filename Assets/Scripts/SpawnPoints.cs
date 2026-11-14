using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    private List<Transform> spawnPoints;

    public List<Transform> SpawnPointsList => spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>().ToList();
        Debug.Log(spawnPoints.Count);
    }

}
