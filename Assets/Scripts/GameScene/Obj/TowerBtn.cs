using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    private Text txtTips;
    private Text goldText;

    private Image ImgTower;

    private void Awake()
    {
        txtTips = this.transform.Find("txtTips").GetComponent<Text>();
        goldText = this.transform.Find("goldText").GetComponent<Text>();

        ImgTower = this.transform.GetComponent<Image>();
    }

   

    public void ShowTower()
    {
        

        int index = this.transform.GetSiblingIndex();

        TowerData towerData = GameDataMgr.Instance.towerDatas[index];

        if (!GameLevelMgr.Instance.nowSelectTowers.Contains(towerData.id))
        {
            GameLevelMgr.Instance.nowSelectTowers.Add(towerData.id);
        }

            goldText.text = "￥"+towerData.money;

        ImgTower.sprite = Resources.Load<Sprite>(towerData.imgRes);
    }
}
