using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private AudioSource audioSource;

    private static BKMusic instance;

    public static BKMusic Instance => instance;



    private void Awake()
    {
        instance = this;

        audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        MusicData musicData = GameDataMgr.Instance.musicData;

        SetMusicOpen(musicData.isOpenMusic);
        SetMusicValue(musicData.isMusicValue);
    }

    public void SetMusicOpen(bool isOpen)
    {
        audioSource.mute = !isOpen;
    }

    public void SetMusicValue(float value)
    {
        audioSource.volume = value;
    }
}
