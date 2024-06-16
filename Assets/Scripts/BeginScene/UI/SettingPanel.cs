using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class SettingPanel : BasePanel
{
    private Toggle musicToggle;
    private Toggle soundToggle;

    private Slider musicSlider;
    private Slider soundSlider;

    private Button closeBtn;

    

    protected override void Awake()
    {
        base.Awake();

        musicToggle = this.transform.Find("SettingBG/Music/Toggle").GetComponent<Toggle>();
        soundToggle = this.transform.Find("SettingBG/Sound/Toggle").GetComponent<Toggle>();

        musicSlider = this.transform.Find("SettingBG/Music/Slider").GetComponent<Slider>();
        soundSlider = this.transform.Find("SettingBG/Sound/Slider").GetComponent<Slider>();

        closeBtn = this.transform.Find("SettingBG/CloseBtn").GetComponent<Button>();

        
    }

    protected override void Init()
    {
        


        musicToggle.onValueChanged.AddListener((b) =>
        {
            BKMusic.Instance.SetMusicOpen(b);
        });

        musicSlider.onValueChanged.AddListener((v) =>
        {
            BKMusic.Instance.SetMusicValue(v);
        });

        closeBtn.onClick.AddListener(() =>
        {
            MusicData musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");

            musicData.isOpenMusic = musicToggle.isOn;
            musicData.isOpenSound = soundToggle.isOn;

            musicData.isMusicValue = musicSlider.value;
            musicData.isSoundValue = soundSlider.value;

            GameDataMgr.Instance.SaveMusicData(musicData);


            UIManager.Instance.CloseThisPanel<SettingPanel>(false);
        });
    }

    public override void ShowThisPanel()
    {
        base.ShowThisPanel();

        MusicData musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");

        musicToggle.isOn = musicData.isOpenMusic;
        soundToggle.isOn = musicData.isOpenSound;

        musicSlider.value = musicData.isMusicValue;
        soundSlider.value = musicData.isSoundValue;

        BKMusic.Instance.SetMusicOpen(musicToggle.isOn);
        BKMusic.Instance.SetMusicValue(musicSlider.value);
    }

    public override void CloseThisPanel(UnityAction unityAction)
    {
        base.CloseThisPanel(unityAction);
    }
}
