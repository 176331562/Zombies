using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager
{
    private static UIManager instance = new UIManager();

    public static UIManager Instance => instance;

    private GameObject canvas;

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private UIManager()
    {
        if(canvas == null)
        {
            canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        }
    }

    public T ShowThisPanel<T>() where T : BasePanel
    {
        if (canvas == null)
        {
            canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        }

        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        if(!panelDic.ContainsKey(panelName))
        {
            GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
            panelObj.name = panelName;
            panelObj.transform.SetParent(canvas.transform,false);

            T panel = panelObj.GetComponent<T>();

            panelDic.Add(panelName,panel);

            panelDic[panelName].ShowThisPanel();

            return panelDic[panelName] as T;
        }
        return null;
    }

    public void CloseThisPanel<T>(bool isFade,UnityAction callBack = null) where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if(panelDic.ContainsKey(panelName))
        {
            if(isFade)
            {
                panelDic[panelName].CloseThisPanel(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);

                    panelDic.Remove(panelName);

                    callBack?.Invoke();
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);

                panelDic.Remove(panelName);
            }
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (!panelDic.ContainsKey(panelName))
            return null;

        return panelDic[panelName] as T;
    }
}
