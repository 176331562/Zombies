using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMain : MonoBehaviour
{
    private GameObject nowSelectRoleObj;

    private Transform roleFather;

    void Start()
    {
        nowSelectRoleObj = GameLevelMgr.Instance.nowRoleObj;
        

        Camera.main.GetComponent<CameraFollow>().SetTarget(nowSelectRoleObj.transform);

        roleFather = GameObject.Find("RoleTra").transform;
        nowSelectRoleObj.transform.localPosition = Vector3.zero;
        nowSelectRoleObj.transform.SetParent(roleFather,false);
        

        GameLevelMgr.Instance.CreateMonsterPoint(0);

        GameLevelMgr.Instance.CreateMonsterPoint(1);

        GameLevelMgr.Instance.CreateMonsterPoint(2);

        GameLevelMgr.Instance.CreateMonsterPoint(3);

        GameLevelMgr.Instance.UpdateMonsterWave();

        MainTowerObject.Instance.InitTower(GameLevelMgr.Instance.playerData.nowSelectSceneInfo);
    }

   
}
