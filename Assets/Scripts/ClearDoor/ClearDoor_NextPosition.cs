using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ����ϸ� Ŭ���� ���������� �̵��ϴ� �Ϳ� ���� �Լ�
/// </summary>
public class ClearDoor_NextPosition : ClearDoor
{
    //public IClear[] iclear;

    [SerializeField]
    protected ClearTrigger trigger;

    [SerializeField]
    Transform movePosition_Transform;

    Vector3 nextPosition;

    protected override ClearTrigger Trigger { get => trigger; set => trigger = value; }

    private void Start()
    {
        nextPosition =  movePosition_Transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(trigger == ClearTrigger.Clear_NextStage)
            {
                NextPosition(other.transform);
            }
        }
    }

    private void NextPosition(Transform transform)
    {
        transform.position = nextPosition;
    }


}

