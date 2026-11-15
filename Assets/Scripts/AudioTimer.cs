using UnityEngine;

public class AudioTimer : MonoBehaviour
{
    [SerializeField] private float delayFromAwake = 0.0f;
    [SerializeField] private AudioSource audioSource;

    private bool played = false;

    void FixedUpdate()
    {
        // Check if timer is out
        if (delayFromAwake <= 0 && !played)
        {
            // Play the audio
            audioSource.Play();
            played = true;
        }
        else
        {
            delayFromAwake -= Time.fixedDeltaTime;
        }
        
    }
}
