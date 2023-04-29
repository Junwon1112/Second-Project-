using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsterPrefabs;

    [SerializeField]
    int monsterCount = 4;
    [SerializeField]
    float min_X_Range = -25.0f;
    [SerializeField]
    float max_X_Range = 25.0f;
    [SerializeField]
    float min_Y_Range = -25.0f;
    [SerializeField]
    float max_Y_Range = 25.0f;

    float delayTime = 10.0f;  //���� ���� �� ���� ���� ���� ������ �ð�

    /// <summary>
    /// ���� ���Ͱ� ���� �� �������������� ����, true�� ���� ������ ������ Ÿ���̶�� ��
    /// </summary>
    public bool IsDelaying { get; private set; }



    //List<GameObject> monsterList = new List<GameObject>();

    private void Start()
    {
        for(int i =0; i < monsterCount; i++)
        {
            MonsterCreate();
        }
        IsDelaying = false;
    }

    private void Update()
    {
        if(transform.childCount < monsterCount && !IsDelaying)
        {
            StartCoroutine(CoCreateDelay(delayTime));
        }
    }

    private void MonsterCreate()
    {
        float randPos_x = Random.Range(min_X_Range, max_X_Range);
        float randPos_y = Random.Range(min_Y_Range, max_Y_Range);
        int monsterRandom = Random.Range(0, monsterPrefabs.Length);  //���° ���͸� ��������

        GameObject obj = Instantiate(monsterPrefabs[monsterRandom], transform.position + new Vector3(randPos_x, 1, randPos_y ), transform.rotation, transform);
    }


    IEnumerator CoCreateDelay(float _delayTime)
    {
        IsDelaying = true;
        yield return new WaitForSeconds(_delayTime);
        MonsterCreate();
        IsDelaying=false;
    }
}
