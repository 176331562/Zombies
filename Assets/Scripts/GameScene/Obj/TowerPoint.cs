using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    private GameObject towerObj;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.GetPanel<GamePanel>().ShowTowerUI(true);
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.GetPanel<GamePanel>().ShowTowerUI(false);
        }

        
    }

    private void Update()
    {
        
    }

    public void CreateTower(int id)
    {
        if(towerObj == null)
        {
            TowerData towerData = GameDataMgr.Instance.towerDatas[id - 1];

            towerObj = GameObject.Instantiate(Resources.Load<GameObject>(towerData.res), this.transform.position, this.transform.rotation);
        }
        else
        {

        }
    }
}
