using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{

    private int nowMonsterDataIndex;
    private MonsterData nowSelectMonster;

    public int nowWaveNum;
    public int maxWaveNum;


    private int monsterNumOneWave;
    private int nowMonsterNum;

    private Transform monsterTra;

    void Start()
    {
        if(monsterTra == null)
        {
            monsterTra = GameObject.Find("MonsterTra").transform;
        }

        CreateWave();
    }

    //创建怪物波数
    public void CreateWave()
    {
        
        

        //当还能创建波数时
        if(maxWaveNum > 0 && nowWaveNum < maxWaveNum)
        {
            //创建波数
            ++nowWaveNum;


            ++GameLevelMgr.Instance.nowWave;
            

            UIManager.Instance.GetPanel<GamePanel>().UpdateWaveUI(GameLevelMgr.Instance.nowWave, GameLevelMgr.Instance.maxWave);

            nowSelectMonster = GameDataMgr.Instance.monsterDatas[nowMonsterDataIndex];

            CreateMonster();
        }
    }

    //创建怪物
    public void CreateMonster()
    {
        //如果当前数量比应该创建的数量少
        if(nowMonsterNum < monsterNumOneWave)
        {
            //创建怪物预制体
            GameObject monsterObj = GameObject.Instantiate(Resources.Load<GameObject>(nowSelectMonster.res),
                this.transform.position,Quaternion.identity);

            monsterObj.transform.SetParent(monsterTra,false);
            monsterObj.name = nowSelectMonster.id.ToString();

            MonsterObject monsterObject = monsterObj.AddComponent<MonsterObject>();
            monsterObject.InitMonster(nowSelectMonster.atk,
                nowSelectMonster.atkOffst, nowSelectMonster.hp,nowSelectMonster.moveSpeed,
                nowSelectMonster.roundSpeed,nowSelectMonster.animator);

            //将当前创建怪物添加到存活的怪物列表中
            GameLevelMgr.Instance.nowLiveMonsterNum.Add(monsterObject);

            ++nowMonsterNum;

            //当一波的怪物数量没创建完就接着创建
            if (nowMonsterNum <= monsterNumOneWave)
            {
                Invoke("CreateMonster", 5);
            }
        }
        else//当前波数的怪物创建完时,就创建波数
        {
            nowMonsterNum = 0;

            Invoke("CreateWave", 5);
        }
    }

    //检查当前怪物点已经出完所有的怪物
    public bool CheckOver()
    {
        //已经是最后一波并且所有的怪物都生成了
        if(nowWaveNum == maxWaveNum && nowMonsterNum == monsterNumOneWave)
        {
            return true;
        }
        return false;
    }

    public void InitMonsterPoint(MonsterPointData monsterPoint)
    {
        this.maxWaveNum = monsterPoint.maxWaveNum;
        this.monsterNumOneWave = monsterPoint.monsterNumOneWave;
        this.nowMonsterDataIndex = monsterPoint.monsterIndex-1;
    }
}
