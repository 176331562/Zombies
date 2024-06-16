using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();

    public static GameLevelMgr Instance => instance;

    public PlayerData playerData;
    public GameObject nowRoleObj;

    public int nowWave;
    public int maxWave;

    //当前场景存活的怪物列表
    public List<MonsterObject> nowLiveMonsterNum = new List<MonsterObject>();

    //当前场景存在的怪物点
    public List<MonsterPoint> monsterPoints = new List<MonsterPoint>();

    //当前可以放置的三种塔
    public List<int> nowSelectTowers = new List<int>();
    private GameLevelMgr()
    {
        //获取到之前保存的玩家数据
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");

        nowWave = 0;
        maxWave = 0;
    }



    //如果当前场景里面的怪物全部死亡
    public bool IsMonsterAllDead()
    {
        return nowLiveMonsterNum.Count <= 0;
    }

    //创建怪物点
    public void CreateMonsterPoint(int index)
    {
        MonsterPointData monsterPointData = GameDataMgr.Instance.monsterPointDatas[index];

        GameObject monsterPointObj = GameObject.Instantiate(Resources.Load<GameObject>(monsterPointData.res));
        monsterPointObj.transform.position = new Vector3(monsterPointData.x, monsterPointData.y, monsterPointData.z);
        monsterPointObj.transform.rotation = Quaternion.Euler(monsterPointData.rotateX, monsterPointData.rotateY, monsterPointData.z);

        MonsterPoint monsterPoint = monsterPointObj.GetComponent<MonsterPoint>();

        monsterPoints.Add(monsterPoint);

        monsterPoint.InitMonsterPoint(monsterPointData);
    }

    public void UpdateMonsterWave()
    {
        for (int i = 0; i < monsterPoints.Count; i++)
        {
            nowWave += monsterPoints[i].nowWaveNum;
            maxWave += monsterPoints[i].maxWaveNum;
        }

        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveUI(nowWave, maxWave);
    }
}
