using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{

    private static MainTowerObject instance;

    public static MainTowerObject Instance => instance;

    private int nowTowerHp;
    private int maxTowerHp;

    private bool isDead = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

   

    public void Wound(int damage)
    {
        Debug.LogError("受伤");   

        //当前防御塔没爆炸
        if(nowTowerHp > 0)
        {
            //当可以抵挡伤害时
            if(nowTowerHp - damage > 0)
            {
                nowTowerHp -= damage;

                UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(nowTowerHp);
            }
            else
            {
                nowTowerHp = 0;

                UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(nowTowerHp);

                Dead();
            }
        }
        else
        {
            nowTowerHp = 0;

            UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(0);

            Dead();
        }
    }

    public void InitTower(SceneData sceneData)
    {
        this.maxTowerHp = sceneData.towerHp;
        this.nowTowerHp = sceneData.towerHp;
    }

    public void Dead()
    {
        isDead = true;

        UIManager.Instance.ShowThisPanel<FinishPanel>().ChangeText("游戏失败");
    }
}
