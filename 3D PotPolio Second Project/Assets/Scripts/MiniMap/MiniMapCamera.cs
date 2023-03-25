using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Vector3 playerPosition;
    [SerializeField]
    float miniMapHeight = 536.0f;

    /// <summary>
    /// playerPosition의 x,z값만 받음
    /// </summary>
    public Vector3 PlayerPosition
    {
        get { return playerPosition; }
        set 
        {
            playerPosition.x = value.x;
            playerPosition.y = miniMapHeight;
            playerPosition.z = value.z;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        MiniMapMove();
    }

    private void MiniMapMove()
    {
        PlayerPosition = GameManager.Instance.MainPlayer.transform.position;
        transform.position = PlayerPosition;
    }
}
