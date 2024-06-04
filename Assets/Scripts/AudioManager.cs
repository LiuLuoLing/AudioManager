using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class FolderInfo
{
    public int index;
    public string name;
    public List<string> audios;
}

[System.Serializable]
public class AudioConfig
{
    public List<FolderInfo> folders;
}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    private AudioSource audioSource;
    private AudioConfig audioConfig;

    private Dictionary<int, FolderInfo> folderMap = new Dictionary<int, FolderInfo>();
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private bool isPlaying = false;
    private bool skipRequested = false;
    private Action onAudioComplete;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = transform.Find("Talk").GetComponent<AudioSource>();
            LoadAudioConfig();
            PreloadAudioClips();
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = transform.Find("Talk").GetComponent<AudioSource>();
    }

    /// <summary>
    /// ������Ƶ�����ļ�
    /// </summary>
    private void LoadAudioConfig()
    {
        string jsonPath = Path.Combine(Application.streamingAssetsPath, "audio_config.json");
        string jsonText = File.ReadAllText(jsonPath);

        audioConfig = JsonUtility.FromJson<AudioConfig>(jsonText);

        foreach (var folderInfo in audioConfig.folders)
        {
            folderMap[folderInfo.index] = folderInfo;
        }
    }

    /// <summary>
    /// Ԥ������Ƶ��Դ
    /// </summary>
    private void PreloadAudioClips()
    {
        foreach (var folderInfo in folderMap.Values)
        {
            foreach (var audioName in folderInfo.audios)
            {
                string path = $"Audio/{folderInfo.name}/{audioName}";
                AudioClip clip = Resources.Load<AudioClip>(path);
                if (clip != null)
                {
                    audioClips[path] = clip;
                }
                else
                {
                    Debug.LogWarning($"û���ҵ���Ƶ�ļ�·��: {path}");
                }
            }
        }
    }

    /// <summary>
    /// ������Ƶ
    /// </summary>
    /// <param name="folderIndex">�ļ�������</param>
    /// <param name="audioIndex">�ļ�����</param>
    /// <param name="playType">��ѡ������loop:ѭ������ oneShot:���β���</param>
    /// <param name="callback">������ɻص�</param>
    public void PlayAudio(int folderIndex, int audioIndex, string playType = "default", Action callback = null)
    {
        if (!folderMap.ContainsKey(folderIndex))
        {
            Debug.LogError($"��Ч���ļ�������: {folderIndex}");
            return;
        }

        FolderInfo folderInfo = folderMap[folderIndex];

        if (audioIndex < 0 || audioIndex >= folderInfo.audios.Count)
        {
            Debug.LogError($"��Ч��Ƶ����: {audioIndex} \n�ļ���: {folderInfo.name}");
            return;
        }

        string folderName = folderInfo.name;
        string audioName = folderInfo.audios[audioIndex];
        string path = $"Audio/{folderName}/{audioName}";

        if (audioClips.ContainsKey(path))
        {
            AudioClip clip = audioClips[path];
            onAudioComplete = callback;
            skipRequested = false;

            switch (playType)
            {
                case "loop":
                    audioSource.loop = true;
                    break;
                case "oneShot":
                    audioSource.PlayOneShot(clip);
                    callback?.Invoke();
                    break;
                default:
                    audioSource.loop = false;
                    break;
            }
            audioSource.clip = clip;
            audioSource.Play();
            isPlaying = true;
            StartCoroutine(CheckAudioCompletion());
        }
        else
        {
            Debug.LogError($"�Ҳ�����Ƶ��Դ:{path}");
        }
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
            onAudioComplete?.Invoke();
        }
    }

    public void SkipAudio()
    {
        if (isPlaying)
        {
            skipRequested = true;
            StopAudio();
        }
    }

    private IEnumerator CheckAudioCompletion()
    {
        while (isPlaying)
        {
            if (!audioSource.isPlaying && !skipRequested)
            {
                isPlaying = false;
                onAudioComplete?.Invoke();
            }
            yield return null;
        }
    }
}
