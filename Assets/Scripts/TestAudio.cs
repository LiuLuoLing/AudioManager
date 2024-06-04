using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    void Start()
    {
        // 在游戏启动时播放第一个音频
        Debug.Log("Starting audio playback...");
        AudioManager.instance.PlayAudio(1, 0, "default", OnAudioComplete);
    }

    void Update()
    {
        // 按下空格键停止当前播放的音频
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Stopping audio playback...");
            AudioManager.instance.StopAudio();
        }

        // 按下S键跳过当前播放的音频
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Skipping audio playback...");
            AudioManager.instance.SkipAudio();
        }

        // 按下P键播放第二个文件夹中的第二段音频，循环播放
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Playing looped audio...");
            AudioManager.instance.PlayAudio(2, 1, "loop", OnAudioComplete);
        }

        // 按下O键播放第二个文件夹中的第三段音频，一次性播放
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Playing one-shot audio...");
            AudioManager.instance.PlayAudio(2, 2, "oneShot", OnAudioComplete);
        }
    }

    void OnAudioComplete()
    {
        Debug.Log("Audio playback completed or skipped.");
    }
}
