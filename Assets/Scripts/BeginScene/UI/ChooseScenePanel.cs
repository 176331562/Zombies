using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseScenePanel : BasePanel
{
    private Button btnLeft;
    private Button btnRight;

    private Button btnStart;
    private Button btnBack;

    private Image ImgScene;

    private Text txtScene;

    private int nowSceneIndex;
    private SceneData nowSceneData;
    private string nowSceneName;

    protected override void Awake()
    {
        base.Awake();

        btnLeft = this.transform.Find("btnLeft").GetComponent<Button>();
        btnRight = this.transform.Find("btnRight").GetComponent<Button>();

        btnStart = this.transform.Find("startBtn").GetComponent<Button>();
        btnBack = this.transform.Find("backBtn").GetComponent<Button>();

        ImgScene = this.transform.Find("sceneImage").GetComponent<Image>();

        txtScene = this.transform.Find("sceneName").GetComponent<Text>();
    }

    protected override void Init()
    {
        //默认显示第一章
        ChangeScene(nowSceneIndex);

        btnLeft.onClick.AddListener(() =>
        {
            nowSceneIndex = --nowSceneIndex < 0 ? GameDataMgr.Instance.sceneData.Count - 1 : nowSceneIndex;

            ChangeScene(nowSceneIndex);
        });

        btnRight.onClick.AddListener(() =>
        {
            nowSceneIndex = ++nowSceneIndex > GameDataMgr.Instance.sceneData.Count-1 ? 0 : nowSceneIndex;

            ChangeScene(nowSceneIndex);
        });

        btnStart.onClick.AddListener(() =>
        {
            PlayerData playerData = GameDataMgr.Instance.playerData;

            playerData.nowSelectSceneInfo = nowSceneData;

            GameDataMgr.Instance.SavePlayerData(playerData);

            UIManager.Instance.CloseThisPanel<ChooseScenePanel>(true, () =>
            {
                AsyncOperation ao = SceneManager.LoadSceneAsync(nowSceneData.sceneName);
                ao.completed += (ao) =>
                {
                    UIManager.Instance.ShowThisPanel<GamePanel>();

                   GameObject roleObj = GameObject.Instantiate(Resources.Load<GameObject>
                        (GameDataMgr.Instance.playerData.nowSelectRoleInfo.res),
                        new Vector3(217.31f, 30.73f, 218.26f),Quaternion.identity);

                    //roleObj.transform.position = new Vector3(217.31f, 30.73f, 218.26f);

                    GameLevelMgr.Instance.nowRoleObj = roleObj;

                    roleObj.name = GameDataMgr.Instance.playerData.nowSelectRoleInfo.tips;

                    GameDataMgr.Instance.playerData = GameDataMgr.Instance.playerData;

                    roleObj.AddComponent<PlayerObject>();


                };
            });
        });

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<ChooseScenePanel>(true, () =>
            {
                UIManager.Instance.ShowThisPanel<ChooseHeroPanel>();
            });
        });      
    }

    public void ChangeScene(int index)
    {
        if(ImgScene.sprite != null)
        {
            ImgScene.sprite = null;
        }

        //获取当前场景的数据
        nowSceneData = GameDataMgr.Instance.sceneData[index];

        //加载场景图片
        ImgScene.sprite = Resources.Load<Sprite>(nowSceneData.imgRes);

        //场景描述
        txtScene.text = "场景名：" + nowSceneData.sceneName + "\n\n" + "描述：" + nowSceneData.tips;
    }
}
