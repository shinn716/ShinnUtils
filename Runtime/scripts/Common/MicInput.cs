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
        _audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        microphoneList.Clear();
        foreach (var device in Microphone.devices)
            microphoneList.Add(device);

        for (int i = 0; i < Microphone.devices.Length; i++)
            if (Microphone.devices[i].Equals("Microphone (Realtek(R) Audio)") || Microphone.devices[i].Equals("麥克風排列 (Realtek(R) Audio)"))
                GetDevicesIndex = i;

        InitMic();
    }
    [ContextMenu("Init")]
    public bool InitMic()
    {
        _audio.clip = Microphone.Start(Microphone.devices[GetDevicesIndex], true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        //while (!(Microphone.GetPosition(null) > 0)) { }
        _audio.Play();
        return true;
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