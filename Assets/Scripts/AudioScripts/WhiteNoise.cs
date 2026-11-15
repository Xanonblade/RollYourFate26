using UnityEngine;
using UnityEngine.SceneManagement;

public class WhiteNoise : MonoBehaviour
{
    private GameObject player;

    private float volumeLevel;

    void Awake()
    {
        // Look for player & Assign it
        player = GameObject.FindWithTag("Player");
        
        //
    }
}
