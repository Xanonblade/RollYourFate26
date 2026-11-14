using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    
    void Start()
    {
        transform.position = LevelManager.SpawnLocation;
    }
}
