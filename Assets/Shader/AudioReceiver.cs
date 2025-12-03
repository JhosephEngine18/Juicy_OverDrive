using System;
using UnityEngine;

public class AudioReceiver : MonoBehaviour
{
    public AudioSource _BackgroundMusic;
    private float[] audioBuffer = new float[1024];
    private int audioLevelID = 0; 
    private void Start()
    {
        audioLevelID = Shader.PropertyToID("_AudioLevel");
    }

    // Update is called once per frame
    void Update()
    {
        _BackgroundMusic.GetOutputData(audioBuffer, 0);
        float sum = 0;
        foreach (float sample in audioBuffer)
        {
            sum += sample * sample;
        }
        
        float rms = Mathf.Lerp(sum, 1, Time.deltaTime);
        if (rms >= 0.023f)
        { 
            Shader.SetGlobalFloat(audioLevelID, 1);
        }
        else
        {
            Shader.SetGlobalFloat(audioLevelID, 0.7f);
        }
    }
}