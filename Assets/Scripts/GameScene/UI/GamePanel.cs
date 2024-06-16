using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePanel : BasePanel
{

    private float ImgHp = 500f;

    public Button btnBack;


    private Text txtWave;

    private Text txtGold;

    private Text txtHp;

    private Text txtNowBulletNum;

    private Transform towerBtnFather;

    //当前防御塔的血量
    private int towerHp;

    //当前防御塔的最大血量
    private int towerMaxHp;

    //当前的金币数量
    private int nowGoldNum;

    //
    private Image nowHpImg;
    private Image nowHpImgBK;

    //private bool isUpdateHp;

    protected override void Awake()
    {
        base.Awake();

        nowHpImg = this.transform.Find("ImageHp").GetComponent<Image>();
        nowHpImgBK = this.transform.Find("ImageHpBK").GetComponent<Image>();

        btnBack = this.transform.Find("btnBack").GetComponent<Button>();

        txtWave = this.transform.Find("txtWave").GetComponent<Text>();

        txtGold = this.transform.Find("gold").GetComponent<Text>();

        txtHp = this.transform.Find("HP").GetComponent<Text>();

        txtNowBulletNum = this.transform.Find("txtBulletNum/nowBulletNum").GetComponent<Text>();

        towerBtnFather = this.transform.Find("Bot").transform;
    }

    public override void Update()
    {
        base.Update();

        
    }

    protected override void Init()
    {
        //先关闭造塔按钮
        towerBtnFather.gameObject.SetActive(false);

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<GamePanel>(true, () =>
            {
                SceneManager.LoadScene("BeginScene");
            });
        });
    }

    //更新血量
    public void UpdateHp(int hp,int maxHp)
    {
        this.towerHp = hp;
        this.towerMaxHp = maxHp;

        txtHp.text = hp + "/" + maxHp;
    }

    //更新金币数量
    public void UpdateGold(int goldNum)
    {
        this.nowGoldNum = goldNum;

        txtGold.text = goldNum.ToString();
    }

    //更新子弹数量
    public void UpdateBullet(int num)
    {
        txtNowBulletNum.text = num.ToString();
    }

    public override void ShowThisPanel()
    {
        //获取当前防御塔的信息
        SceneData nowSceneData = GameLevelMgr.Instance.playerData.nowSelectSceneInfo;

        //更新防御塔的血量
        UpdateHp(nowSceneData.towerHp, nowSceneData.towerHp);

        //更新当前场景金币数量
        UpdateGold(nowSceneData.money);


        base.ShowThisPanel();
    }

    public void UpdateTowerHp(int nowHp)
    {
        towerHp = nowHp;

        txtHp.text = nowHp + "/" + towerMaxHp;

        (nowHpImg.transform as RectTransform).sizeDelta = new Vector2(ImgHp / 100 * nowHp, (nowHpImg.transform as RectTransform).sizeDelta.y);

        //isUpdateHp = true;
        
    }

    public void ShowTowerUI(bool b)
    {
        //List<TowerData> towerDatas = GameDataMgr.Instance.towerDatas;

        for (int i = 0; i < towerBtnFather.childCount; i++)
        {
            towerBtnFather.GetChild(i).GetComponent<TowerBtn>().ShowTower();
        }

        towerBtnFather.gameObject.SetActive(b);
    }

    public void UpdateWaveUI(int nowWave, int maxWave)
    {
        txtWave.text = nowWave +"/"+ maxWave;
    }
}
