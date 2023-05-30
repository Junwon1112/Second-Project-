using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlayer : MonoBehaviour
{
    JobType jobType;
    [SerializeField]
    ScriptableObj_JobData jobData;

    [SerializeField]
    GameObject[] selectedPlayer;

    Dictionary<JobType, GameObject> dic_Player = new Dictionary<JobType, GameObject>();

    private void Awake()
    {
        jobType = jobData.jobType;
        dic_Player.Add(JobType.SwordMan, selectedPlayer[0]);
        dic_Player.Add(JobType.Witch, selectedPlayer[1]);
    }

    void Start()
    {
        GameObject playerObj = Instantiate(dic_Player[jobData.jobType]);
        Player tempPlayer = playerObj.transform.GetComponent<Player>();
        tempPlayer.Job = jobData.jobType;
    }


}
