using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        try
        {
            transform.position = LevelManager.SpawnLocation;
            transform.rotation = LevelManager.SpawnRotation;
        }
        catch (NullReferenceException e)
        {
            transform.position = Vector3.zero;
            transform.rotation = transform.rotation;
        }
    }
}
