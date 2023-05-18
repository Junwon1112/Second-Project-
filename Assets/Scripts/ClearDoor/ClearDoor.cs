using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ClearDoor : MonoBehaviour
{
    protected virtual ClearTrigger Trigger { get; set; }
}

public enum ClearTrigger
{
    Clear_NextStage = 0,
}