using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    private int atk;

    private int nowBulletNum;
    private int playerBulletNum;

    public Transform firePoint;

    void Start()
    {
        animator = this.GetComponent<Animator>();

        InitPlayerData();
    }

    // Update is called once per frame
    void Update()
    {

        //人物移动
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));

        //人物跟随鼠标X轴进行旋转
        this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));

        //如果按下鼠标左键就攻击
        if(Input.GetMouseButtonDown(0))
        {
            if(nowBulletNum > 0 || playerBulletNum <= 0)
            {
                animator.SetTrigger("Atk");

                if(playerBulletNum > 0)
                {
                    --nowBulletNum;
                }
                

                UIManager.Instance.GetPanel<GamePanel>().UpdateBullet(nowBulletNum);
            }

            
        }

        //如果按下左shift就蹲着走
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 1);
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 0);
        }

        //如果按下空格键就翻滚
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Roll");
        }
    }

    //初始化玩家武器
    public void InitPlayerData()
    {
        //读取到当前的人物数据
        RoleData nowRoleInfo = GameDataMgr.Instance.playerData.nowSelectRoleInfo;

        //得到人物的武器数据
        WeaponData nowWeaponData = GameDataMgr.Instance.weaponDatas[nowRoleInfo.id-1];
            
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(nowWeaponData.animator);

        this.atk = nowWeaponData.atk;
        this.playerBulletNum = nowWeaponData.bulletNum;
        this.nowBulletNum = playerBulletNum;


        UIManager.Instance.GetPanel<GamePanel>().UpdateBullet(nowWeaponData.bulletNum);
    }

    //人物攻击动画事件
    public void AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward+this.transform.up, 1,1<<LayerMask.NameToLayer("Monster"));

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "Monster")
            {
                MonsterObject monsterObject = colliders[i].GetComponent<MonsterObject>();

                monsterObject.Wound(atk);

                break;
            }
        }
    }

    //
    public void ShootEvent()
    {
        if (firePoint == null)
        {
            firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;
        }

        Debug.DrawRay(firePoint.transform.position, firePoint.forward * 10 + firePoint.up * 5, Color.red, 5f);

        RaycastHit[] raycasts = Physics.RaycastAll(firePoint.transform.position, firePoint.forward * 10 + firePoint.up * 5, 10, 1 << LayerMask.NameToLayer("Monster"));

        for (int i = 0; i < raycasts.Length; i++)
        {
            if(raycasts[i].collider.CompareTag("Monster"))
            {
                raycasts[i].collider.gameObject.GetComponent<MonsterObject>().Wound(atk);

                break;
            }
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Debug.DrawRay(firePoint.transform.position, firePoint.transform.position + firePoint.forward,Color.red);
    //}
}
