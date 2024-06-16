using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public abstract class BasePanel : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    private bool isShow = false;

    private float aphlaSpeed = 10;

    private UnityAction callBack;

    protected virtual void Awake()
    {
        if (canvasGroup == null)
           canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

   public virtual void Start()
    {
        Init();
    }

    // Update is called once per frame
   public virtual void Update()
    {
        if(isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += aphlaSpeed * Time.deltaTime;

            if(canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if(!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= aphlaSpeed * Time.deltaTime;

            if(canvasGroup.alpha <=  0)
            {
                canvasGroup.alpha = 0;

                callBack?.Invoke();

                
            }
        }
    }

    protected abstract void Init();

    public virtual void ShowThisPanel()
    {
        //this.gameObject.SetActive(true);

        isShow = true;

        canvasGroup.alpha = 0;
    }

    public virtual void CloseThisPanel(UnityAction unityAction)
    {
        //this.gameObject.SetActive(false);

        isShow = false;

        canvasGroup.alpha = 1;

        callBack = unityAction;
    }


}
