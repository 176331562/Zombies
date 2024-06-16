using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraMove : MonoBehaviour
{
    private Animator animator;

    private UnityAction callBack;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public void TurnLeft(UnityAction unityAction)
    {
        animator.SetTrigger("TurnLeft");

        callBack = unityAction;
    }

    public void TurnRight(UnityAction unityAction)
    {
        animator.SetTrigger("TurnRight");

        callBack = unityAction;
    }

    public void CallBackEvent()
    {
        if(callBack != null)
        {
            callBack?.Invoke();
        }
    }
}
