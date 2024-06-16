using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();

    public static GameDataMgr Instance => instance;

    public MusicData musicData;

    public List<RoleData> roleDatas;

    //玩家仓库数据
    public PlayerData playerData;

    //加载场景列表
    public List<SceneData> sceneData;

    //加载武器库
    public List<WeaponData> weaponDatas;

    //加载怪物数据
    public List<MonsterData> monsterDatas;

    //加载怪物点数据
    public List<MonsterPointData> monsterPointDatas;

    //加载塔的数据表
    public List<TowerData> towerDatas;

    private GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");

        roleDatas = JsonMgr.Instance.LoadData<List<RoleData>>("RoleData");

        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");

        sceneData = JsonMgr.Instance.LoadData<List<SceneData>>("SceneData");

        weaponDatas = JsonMgr.Instance.LoadData<List<WeaponData>>("WeaponData");

        monsterDatas = JsonMgr.Instance.LoadData<List<MonsterData>>("MonsterData");

        monsterPointDatas = JsonMgr.Instance.LoadData<List<MonsterPointData>>("MonsterPointData");

        towerDatas = JsonMgr.Instance.LoadData<List<TowerData>>("TowerData");
    }

    public void SaveMusicData(MusicData musicData)
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SavePlayerData(PlayerData playerData)
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
}
