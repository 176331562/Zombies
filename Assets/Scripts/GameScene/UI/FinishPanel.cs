using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishPanel : BasePanel
{
    private Text contentText;

    private Button closeBtn;

    protected override void Awake()
    {
        base.Awake();

        contentText = this.transform.Find("FinishPanelBK/ContentText").GetComponent<Text>();

        closeBtn = this.transform.Find("FinishPanelBK/CloseButton").GetComponent<Button>();
    }

    protected override void Init()
    {
        closeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<FinishPanel>(false);
            UIManager.Instance.CloseThisPanel<GamePanel>(false);


            SceneManager.LoadScene(0);
        });
    }

    public void ChangeText(string text)
    {
        contentText.text = text;
    }
}
