using System.Collections.Generic;
using UnityEngine;

public enum SFXID
{
    trainChug,
    trainChooChoo,
    trainBell
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    [SerializeField] public List<AudioFile> SFX;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource.GetComponent<AudioSource>();
    }

    [System.Serializable]
    public struct AudioFile
    {
        [SerializeField]
        public readonly SFXID ID;
        [SerializeField]
        public readonly AudioClip Clip;
    }
}
