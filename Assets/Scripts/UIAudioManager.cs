using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIAudioManager : MonoBehaviour
{
    [Header("-----音量设置条-----")]
    [SerializeField] Slider MasterSlider;
    [SerializeField] Slider TalkSlider;
    [SerializeField] Slider BgSlider;

    [Header("-----音频资源-----")]
    [SerializeField] AudioClip oneShotClip;
    [SerializeField] AudioClip bgClip;

    [Header("-----播放按钮-----")]
    [SerializeField] Button oneShotBtn;
    [SerializeField] Button bgBtn;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource talkSource;
    [SerializeField] AudioSource bgSource;



    void Start()
    {
        oneShotBtn.onClick.AddListener(PlayOneShot);
        bgBtn.onClick.AddListener(PlayBg);

    }

    void PlayOneShot()
    {
        talkSource.clip = oneShotClip;
        talkSource.Play();
    }

    void PlayBg()
    {
        bgSource.clip = bgClip;
        bgSource.Play();
    }
}
