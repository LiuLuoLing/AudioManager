using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class AudioConfigGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Audio Config")]
    public static void GenerateAudioConfig()
    {
        string audioFolderPath = "Assets/Resources/Audio";
        string configFilePath = Path.Combine(Application.streamingAssetsPath, "audio_config.json");

        List<FolderInfo> folders = new List<FolderInfo>();

        string[] folderPaths = Directory.GetDirectories(audioFolderPath);
        foreach (string folderPath in folderPaths)
        {
            DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
            FolderInfo folder = new FolderInfo();
            folder.index = folders.Count + 1;
            folder.name = folderInfo.Name;
            folder.audios = new List<string>();

            string[] audioFiles = Directory.GetFiles(folderPath);
            foreach (string audioFile in audioFiles)
            {
                if (audioFile.EndsWith(".mp3") || audioFile.EndsWith(".wav") || audioFile.EndsWith(".ogg"))
                {
                    folder.audios.Add(Path.GetFileNameWithoutExtension(audioFile));
                }
            }

            folders.Add(folder);
        }

        AudioConfig audioConfig = new AudioConfig();
        audioConfig.folders = folders;

        string json = JsonUtility.ToJson(audioConfig, true);
        File.WriteAllText(configFilePath, json);

        Debug.Log($"Audio config generated at: {configFilePath}");
    }
}