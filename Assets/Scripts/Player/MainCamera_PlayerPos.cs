using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_PlayerPos : MonoBehaviour
{
    public static MainCamera_PlayerPos instance;

    Vector3 playerPos;
    float lerpRate = 15.0f;

    public MainCamera_PlayerPos Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        CameraMoving();
    }

    public void CameraMoving()
    {
        playerPos = InGameManager.Instance.MainPlayer.transform.position;
        transform.position = Vector3.Lerp(transform.position, playerPos, lerpRate * Time.deltaTime);
        transform.rotation = InGameManager.Instance.MainPlayer.transform.rotation;
    }
}
