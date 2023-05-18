using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 문을 통과하면 클리어 스테이지로 이동하는 것에 관한 함수
/// </summary>
public class ClearDoor_NextStage : ClearDoor
{
    //public IClear[] iclear;

    [SerializeField]
    protected ClearTrigger trigger;

    protected override ClearTrigger Trigger { get => trigger; set => trigger = value; }

    //private void Start()
    //{
    //    //트리거 enum 순서로 정렬 
    //    for(int i = 0; i < iclear.Length-1; i++)
    //    {
    //        int index = i;
    //        for(int j = 1; j < iclear.Length - index; j++)
    //        {
    //            if((int)iclear[i].Trigger > (int)iclear[i+j].Trigger)
    //            {
    //                IClear tempClear = iclear[i];
    //                iclear[i] = iclear[i + 1];
    //                iclear[i + 1] = tempClear;

    //            }
    //        }
    //    }

    //}


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(trigger == ClearTrigger.Clear_NextStage)
            {
                NextStage();
            }
        }
    }

    private void NextStage()
    {
        SceneManager.LoadScene(GameManager.Instance.CurrentScene.buildIndex + 1);
    }


}


