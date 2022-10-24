using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //몬스터가 해야할 일들
    //1, 일정 구간 순찰
    //2. 순찰 중 일정범위 내 플레이어 존재시 추적
    //3. 너무 먼 거리를 오면 제자리로 돌아감
    //4. 움직임, 공격, 맞음, 죽음 4가지 상태
    //5. 체력 필요 => 체력 체크 용 인터페이스 필요

    NavMeshAgent agent;
    Transform[] patrolPoints;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }

    private void Start()
    {
        Transform patrolPoint = GameObject.FindGameObjectWithTag("PatrolPoint").transform.GetComponent<Transform>();

        patrolPoints = new Transform[patrolPoint.childCount];

        for (int i = 0; i < patrolPoint.childCount; i++)
        {
            patrolPoints[i] = patrolPoint.transform.GetChild(i);
            int j = 0;
        }



        transform.position = patrolPoints[0].transform.position;
        
    }

    private void Update()
    {
        
    }

    private void SetPatrol()
    {
        
    }
}
