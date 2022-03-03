// https://github.com/keijiro/UnityMicTest

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicInput : MonoBehaviour
{
    public static MicInput instance;

    public int GetDevicesIndex = 0;
    public List<string> microphoneList = new List<string>();
    public float sensitivity = 1;
    public float loudness = 0;
    public bool mute = true;

    private float squareSum;
    private int sampleCount;
    private AudioSource _audio;

    void Awake()
    { 
        instance = this;
    }

    IEnumerator Start()
    {
        AudioSettings.speakerMode = AudioSpeakerMode.Mono;
        AudioSettings.outputSampleRate = 44100;
        AudioSettings.SetDSPBufferSize(320, 4);
        yield return null;
        _audio = GetComponent<AudioSource>();

        yield return null;
        microphoneList.Clear();
        foreach (var device in Microphone.devices)
            microphoneList.Add(device);

        for (int i = 0; i < Microphone.devices.Length; i++)
            if (Microphone.devices[i].Equals("Microphone (Realtek(R) Audio)") || Microphone.devices[i].Equals("麥克風排列 (Realtek(R) Audio)"))
                GetDevicesIndex = i;

        yield return null;
        InitMic();
    }

    [ContextMenu("Init")]
    public void InitMic()
    {
        _audio.Stop();
        _audio.loop = true;
        _audio.mute = false;

        int minFreq, maxFreq;
        Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

        _audio.clip = Microphone.Start(Microphone.devices[GetDevicesIndex], true, 10, 44100);

        while (_audio.clip != null)
        {
            int delay = Microphone.GetPosition(null);
            if (delay > 0)
            {
                _audio.Play();
                Debug.Log("Latency = " + (1000.0f / GetComponent<AudioSource>().clip.frequency * delay) + " msec");
                break;
            }
        }
    }
    [ContextMenu("Dispose")]
    public void Dispose()
    {
        loudness = 0;
        _audio.Stop();
    }

    private void Update()
    {
        var rms = (sampleCount > 0) ? Mathf.Sqrt(squareSum / sampleCount) * sensitivity : 0;
        loudness = rms;
        squareSum = 0;
        sampleCount = 0;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (var i = 0; i < data.Length; i += channels)
        {
            var level = data[i];
            squareSum += level * level;
        }

        sampleCount += data.Length / channels;

        if (mute)
            for (var i = 0; i < data.Length; i++)
                data[i] = 0;
    }
}