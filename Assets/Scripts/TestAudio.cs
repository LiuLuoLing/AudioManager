using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    void Start()
    {
        // ����Ϸ����ʱ���ŵ�һ����Ƶ
        Debug.Log("Starting audio playback...");
        AudioManager.instance.PlayAudio(1, 0, "default", OnAudioComplete);
    }

    void Update()
    {
        // ���¿ո��ֹͣ��ǰ���ŵ���Ƶ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Stopping audio playback...");
            AudioManager.instance.StopAudio();
        }

        // ����S��������ǰ���ŵ���Ƶ
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Skipping audio playback...");
            AudioManager.instance.SkipAudio();
        }

        // ����P�����ŵڶ����ļ����еĵڶ�����Ƶ��ѭ������
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Playing looped audio...");
            AudioManager.instance.PlayAudio(2, 1, "loop", OnAudioComplete);
        }

        // ����O�����ŵڶ����ļ����еĵ�������Ƶ��һ���Բ���
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
