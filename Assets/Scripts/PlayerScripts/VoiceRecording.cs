using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceRecording : MonoBehaviour
{
    [SerializeField] private AudioClip testAudioClip;
    [SerializeField] private AudioClip trueAudioClip;

    [Header("Microphone Settings")]
    public float recordingSeconds = 3;
    public int sampleRate = 16000;

    private float timer = 0;

    float[] clipSampleData = new float[16000];

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
        string micName = Microphone.devices[0];

        timer += Time.deltaTime;

        if (timer >= recordingSeconds && Microphone.IsRecording(micName))
        {
            Microphone.End(micName);

            GetComponent<RunWhisper>().audioClip = trueAudioClip;
            GetComponentInParent<WhisperProcess>().StartParse();
        }
        /*
        string micName = Microphone.devices[0];

        //Debug.Log("Using microphone: " + micName);
        //Debug.Log("Recording...");

        if (timer >= recordingSeconds)
        {
            timer = 0;

            if (Microphone.IsRecording(micName))
            {
                Microphone.End(micName);

                testAudioClip.GetData(clipSampleData, 0);

                float clipLoudness = 0f;
                foreach (var sample in clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= testAudioClip.channels * testAudioClip.samples; //clipLoudness is what you are looking for

                Debug.Log(clipLoudness);
            }

            testAudioClip = Microphone.Start(micName, false, 1, sampleRate);

        }
        */

        
    }

    public void StartRecording()
    {
        string micName = Microphone.devices[0];

        if (!Microphone.IsRecording(micName))
        {
            trueAudioClip = Microphone.Start(micName, false, 3, sampleRate);
            timer = 0;
        }
        
    }
}
