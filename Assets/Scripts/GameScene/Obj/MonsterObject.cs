using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterObject : MonoBehaviour
{
    #region 怪物数据

    private int atk;

    private float atkOffset;
    private float nowAtkOffset;

    private int moveSpeed;

    private int rotateSpeed;

    private int hp;

    #endregion
    private Animator animator;

    private NavMeshAgent navMeshAgent;

    public bool isDead = false;

    

    void Start()
    {
        
    }


    void Update()
    {
        //怪物到塔前时
        if(Vector3.Distance(this.transform.position,MainTowerObject.Instance.transform.position) <= 3f)
        {
            //到了塔前看着塔攻击
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(MainTowerObject.Instance.transform.position - this.transform.position),
                rotateSpeed * Time.deltaTime);

            this.navMeshAgent.isStopped = true;
            this.animator.SetBool("Walk", false);

            //攻击冷却好了
            if(nowAtkOffset - Time.deltaTime <= 0)
            {
                nowAtkOffset = atkOffset;

                //怪物每一次攻击防御塔都要掉血
                this.animator.SetTrigger("Atk");
            }           
        }
    }

    public void InitMonster(int atk,float atkOffset,int hp,int moveSpeed,int rotateSpeed,string animatorName)
    {
        this.atk = atk;
        this.atkOffset = atkOffset;
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;
        this.hp = hp;

        this.animator = this.GetComponent<Animator>();
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();

        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animatorName);

        navMeshAgent.speed = moveSpeed;
        navMeshAgent.angularSpeed = rotateSpeed;
        

    }

    public void Wound(int damage)
    {
        if (isDead)
            return;

        if(hp - damage > 0)
        {
            animator.SetTrigger("Wound");

            this.hp -= damage;
        }
        else
        {
            //死亡
            Dead();
        }
    }

    

    public void Dead()
    {
        isDead = true;

        animator.SetTrigger("Dead");

        //从当前存活的怪物列表中删除
        GameLevelMgr.Instance.nowLiveMonsterNum.Remove(this);

        Debug.LogError(GameLevelMgr.Instance.nowLiveMonsterNum.Count);

        Destroy(this.gameObject, 3);

        //场景上的怪物已经死完了
        if(GameLevelMgr.Instance.IsMonsterAllDead())
        {
            //游戏胜利
            UIManager.Instance.ShowThisPanel<FinishPanel>().ChangeText("游戏胜利");

        }
    }


    #region 怪物攻击动画
    //怪物动画事件
    public void BornOver()
    {      
        animator.SetBool("Walk", true);

        this.navMeshAgent.SetDestination(MainTowerObject.Instance.transform.position);
    }

    public void AtkEvent()
    {
        //获取怪物面板的碰撞器
        Collider[] colliders = Physics.OverlapBox(this.transform.position,
            this.transform.forward, Quaternion.identity, 1 << LayerMask.NameToLayer("MainTower"));

        //看看是不是有防御塔
        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.LogError(colliders[i].name);

            if(colliders[i].gameObject == MainTowerObject.Instance.gameObject)
            {
                MainTowerObject.Instance.Wound(this.atk);
            }
        }
    }

    #endregion
}
