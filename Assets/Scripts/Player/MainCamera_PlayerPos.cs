using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_PlayerPos : MonoBehaviour
{
    Vector3 playerPos;
    float lerpRate = 15.0f;

    void Update()
    {
        CameraMoving();
    }

    public void CameraMoving()
    {
        playerPos = GameManager.Instance.MainPlayer.transform.position;
        //transform.position = Vector3.Lerp(transform.position, playerPos, lerpRate * Time.deltaTime);
        //transform.rotation = GameManager.Instance.MainPlayer.transform.rotation;
    }
}
