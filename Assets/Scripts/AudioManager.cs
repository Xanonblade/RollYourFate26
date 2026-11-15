using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");

        if (obj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Play the audio sources in the game manager
    /// </summary>
    public void PlayFireSpell()
    {
        this.gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
        this.gameObject.transform.GetChild(1).GetComponent<AudioSource>().Play();
    }

    public void PlayGrowSpell()
    {
        this.gameObject.transform.GetChild(2).GetComponent<AudioSource>().Play();
    }
}
