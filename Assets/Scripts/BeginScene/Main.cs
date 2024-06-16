using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    
    void Start()
    {
        UIManager.Instance.ShowThisPanel<BeginPanel>();

        Debug.LogError(Application.persistentDataPath);
    }

   
}
