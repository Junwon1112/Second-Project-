using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =("new JobData"), menuName = ("Scriptable Object_Job Data/Job_Data"), order = 1) ]
public class ScriptableObj_JobData : ScriptableObject
{
    public JobType jobType;
}
