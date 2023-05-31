using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationReset : MonoBehaviour
{
    public static LocationReset instance;

    Vector3 rePosLocation_Player;
    //Vector3 rePosLocation_Monster;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        rePosLocation_Player = GameObject.Find("StartingPoint").transform.position;
        //rePosLocation_Player = new Vector3(-67, 5, 120);
        //rePosLocation_Monster = new Vector3(0, 5, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = rePosLocation_Player;
        }
        //else if(other.CompareTag("Monster"))
        //{
        //    other.transform.parent.transform.position = rePosLocation_Monster;
        //}

    }

    private void OnLevelWasLoaded(int level)
    {
        rePosLocation_Player = GameObject.Find("StartingPoint").transform.position;
    }
}
