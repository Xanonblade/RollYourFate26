using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceRecording : MonoBehaviour
{
    [SerializeField] private AudioClip testAudioClip;
    [SerializeField] private AudioClip trueAudioClip;

    [Header("Microphone Settings")]
    public float recordingSeconds = 2.5f;
    public int sampleRate = 16000;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
        if (Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            Debug.Log("Microphone found");

            foreach (var device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
            }

        }
        else
        {
            throw new System.Exception("No Microphone Found!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Awaitable StartRecording()
    {

        string micName = Microphone.devices[0];

        if (!Microphone.IsRecording(micName))
        {
            trueAudioClip = Microphone.Start(micName, false, 5, sampleRate);
        }

        await Awaitable.WaitForSecondsAsync(recordingSeconds * Time.timeScale);

        Microphone.End(micName);

        GetComponent<RunWhisper>().audioClip = trueAudioClip;
        GetComponentInParent<WhisperProcess>().StartParse();
    }
}
