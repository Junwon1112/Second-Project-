using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //���Ͱ� �ؾ��� �ϵ�
    //1, ���� ���� ����
    //2. ���� �� �������� �� �÷��̾� ����� ����
    //3. �ʹ� �� �Ÿ��� ���� ���ڸ��� ���ư�
    //4. ������, ����, ����, ���� 4���� ����
    //5. ü�� �ʿ� => ü�� üũ �� �������̽� �ʿ�

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
