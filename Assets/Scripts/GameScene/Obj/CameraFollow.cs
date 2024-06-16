using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    private Vector3 nowPos;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private float moveSpeed = 10;
    private float rotateSpeed = 10;

    private Quaternion targetQ;

 
    // Update is called once per frame
    void Update()
    {
        nowPos = target.position;

        nowPos += target.right * offsetX;

        nowPos += Vector3.up * offsetY;

        nowPos += target.forward * offsetZ;

        this.transform.position = Vector3.Lerp(this.transform.position, nowPos, moveSpeed * Time.deltaTime);

        targetQ = Quaternion.LookRotation(target.transform.position-this.transform.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, rotateSpeed * Time.time);
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }
}
