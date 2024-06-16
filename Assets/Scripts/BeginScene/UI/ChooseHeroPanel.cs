using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{

    private Text txtRoleTitle;
    private Text txtGold;
    private Text txtUnlock;

    private Button btnLeft;
    private Button btnRight;

    private Button btnStart;
    private Button btnBack;

    private Button btnUnlock;

    private int nowRoleIndex;
    private RoleData nowRoleData;
    private GameObject roleObj;

    private Transform roleTra;

    protected override void Awake()
    {
        base.Awake();

        txtRoleTitle = this.transform.Find("heroName").GetComponent<Text>();
        txtGold = this.transform.Find("textTitle_money/money").GetComponent<Text>();

        btnLeft = this.transform.Find("btnLeft").GetComponent<Button>();
        btnRight = this.transform.Find("btnRight").GetComponent<Button>();

        btnStart = this.transform.Find("startBtn").GetComponent<Button>();
        btnBack = this.transform.Find("backBtn").GetComponent<Button>();

        btnUnlock = this.transform.Find("backUnlock").GetComponent<Button>();

        txtUnlock = btnUnlock.transform.Find("txtUnlock").GetComponent<Text>();

        roleTra = GameObject.Find("RoleTra").transform;
    }

    protected override void Init()
    {
        ChangeHero(0);

        btnLeft.onClick.AddListener(() =>
        {
            nowRoleIndex = --nowRoleIndex < 0 ? GameDataMgr.Instance.roleDatas.Count - 1 : nowRoleIndex;

            ChangeHero(nowRoleIndex);
        });

        btnRight.onClick.AddListener(() =>
        {
            nowRoleIndex = ++nowRoleIndex > GameDataMgr.Instance.roleDatas.Count - 1 ? 0 : nowRoleIndex;

            ChangeHero(nowRoleIndex);
        });

        btnStart.onClick.AddListener(() =>
        {
            

            UIManager.Instance.CloseThisPanel<ChooseHeroPanel>(true, () =>
            {
                UIManager.Instance.ShowThisPanel<ChooseScenePanel>();
            });
            
        });

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<ChooseHeroPanel>(true, () =>
            {
                Camera.main.GetComponent<CameraMove>().TurnRight(() =>
                {
                    UIManager.Instance.ShowThisPanel<BeginPanel>();
                });
            });
        });

        btnUnlock.onClick.AddListener(() =>
        {
            BuyHero(nowRoleIndex);
        });
    }

    private void ChangeHero(int index)
    {
        //nowRoleIndex = index;
        //获取当前的玩家仓库数据
        PlayerData playerData = GameDataMgr.Instance.playerData;

        //得到当前选择的人物数据
        nowRoleData = GameDataMgr.Instance.roleDatas[index];

        if(roleObj != null)
        {
            Destroy(roleObj);
        }

        roleObj = GameObject.Instantiate(Resources.Load<GameObject>(nowRoleData.res),roleTra.transform.position,roleTra.rotation);
        roleObj.transform.SetParent(roleTra);

        txtRoleTitle.text = nowRoleData.tips;


        //如果人物不是免费的并且不在我的仓库里
        if(nowRoleData.lockMoney > 0 && !GameDataMgr.Instance.playerData.haveRoles.Contains(nowRoleData.id))
        {

            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "￥" + nowRoleData.lockMoney;
        }
        else if(nowRoleData.lockMoney <= 0)
        {
            if(!playerData.haveRoles.Contains(nowRoleData.id))
            {
                playerData.haveRoles.Add(nowRoleData.id);
            }

            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "已拥有";
        }
        else if(nowRoleData.lockMoney > 0 && GameDataMgr.Instance.playerData.haveRoles.Contains(nowRoleData.id))
        {
            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "已拥有";
        }

        txtGold.text = GameDataMgr.Instance.playerData.haveMoney.ToString();
    }

    //购买人物
    public void BuyHero(int index)
    {
        RoleData roleData = GameDataMgr.Instance.roleDatas[index];

        //获取当前的玩家仓库数据
        PlayerData playerData = GameDataMgr.Instance.playerData;

        if(!playerData.haveRoles.Contains(nowRoleData.id))
        {
            //如果钱不够
            if (playerData.haveMoney < roleData.lockMoney)
                return;

            playerData.haveMoney -= roleData.lockMoney;

            txtGold.text = playerData.haveMoney.ToString();

            playerData.haveRoles.Add(nowRoleData.id);

            txtUnlock.text = "已拥有";

            GameDataMgr.Instance.SavePlayerData(playerData);
        }
        else
        {
            txtUnlock.text = "已拥有";

            //GameDataMgr.Instance.SavePlayerData(playerData);
        }      
    }

    public override void CloseThisPanel(UnityAction unityAction)
    {
        PlayerData playerData = GameDataMgr.Instance.playerData;

        playerData.nowSelectRoleInfo = nowRoleData;

        GameDataMgr.Instance.SavePlayerData(playerData);

        base.CloseThisPanel(unityAction);

        Destroy(roleObj);

        roleObj = null;


    }
}
