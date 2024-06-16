using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    private Button btnStart;
    private Button btnSetting;
    private Button btnAbout;
    private Button btnQuit;

    protected override void Awake()
    {
        base.Awake();


        btnStart = this.transform.Find("btnStart").GetComponent<Button>();
        btnSetting = this.transform.Find("btnSetting").GetComponent<Button>();
        btnAbout = this.transform.Find("btnAbout").GetComponent<Button>();
        btnQuit = this.transform.Find("btnQuit").GetComponent<Button>();
    }

    protected override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<BeginPanel>(true, () =>
            {
                Camera.main.GetComponent<CameraMove>().TurnLeft(() =>
                {
                    UIManager.Instance.ShowThisPanel<ChooseHeroPanel>();
                });
            });
        });

        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowThisPanel<SettingPanel>();
        });

        btnAbout.onClick.AddListener(() =>
        {

        });

        btnQuit.onClick.AddListener(() =>
        {

        });
    }
}
