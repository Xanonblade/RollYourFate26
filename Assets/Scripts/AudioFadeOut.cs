using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    private float startValue;
    [SerializeField] private float finalValue = 0f;
    [SerializeField] private float startInterval = 0f;
    [SerializeField] private float endInterval = 1f;
    [SerializeField] private AudioSource audioSource;
    private float currentTime; // Current time of the scene, used to start/end 
    private float fadeOutTime; // How long it takes for the fade out to occur
    private float decrementValue; // How much the audio will decrease each frame

    void Awake()
    {
        // Initialize currentTime & fadeOutTime & startValue
        currentTime = 0f;
        fadeOutTime = endInterval - startInterval;
        startValue = audioSource.volume;

        // Calculate decrementValue, should be negative number
        decrementValue = (finalValue - startValue) / (fadeOutTime / 0.02f);
    }

    void FixedUpdate()
    {
        if (currentTime >= startInterval && currentTime <= endInterval)
        {
            // Decrement volume level
            audioSource.volume += decrementValue;
        }
        
        // alter time
        currentTime += Time.fixedDeltaTime;
    }
}
