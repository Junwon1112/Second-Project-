using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGarden : MonoBehaviour
{
    [SerializeField]
    GameObject orangeMushroom;

    [SerializeField]
    GameObject purpleMushroom;

    [SerializeField]
    int totalMushroomNum = 30;
    
    int remainMushroomNum;

    int mushroomGarden_RawNum = 6;
    int mushroomGarden_ColumnNum = 5;

    float mushroomPosition_X;
    float mushroomPosition_Z;

    List<Vector3> mushroomPositionsList = new List<Vector3>();

    private void Start()
    {
        int randomNum_PurpleMush = Random.Range(0, 30); // 보라색 버섯이 생성될 위치

        //버섯들 위치 설정
        for (int i = 0 ; i < mushroomGarden_RawNum; i++)
        {   
            mushroomPosition_X = ((transform.position.x - mushroomGarden_RawNum * 0.5f * 3 )+ 3 * (i));

            for(int j = 0; j < mushroomGarden_ColumnNum; j++)
            {
                mushroomPosition_Z = ((transform.position.z - mushroomGarden_ColumnNum * 0.5f * 7) + 7 * j);
                int totalNum = i + j * 5;
                mushroomPositionsList.Add(new Vector3(mushroomPosition_X, 1, mushroomPosition_Z));
            }
        }

        //노랑 버섯 생성
        for (int i = 0; i < totalMushroomNum; i++)
        {
            if(i != randomNum_PurpleMush)
            {
                Instantiate(orangeMushroom, mushroomPositionsList[i], Quaternion.identity, transform);
            }
        }

        Instantiate(purpleMushroom, mushroomPositionsList[randomNum_PurpleMush], Quaternion.identity, transform);
    }
}
