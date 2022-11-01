using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 만드는 메소드의 집합(MakeItem)
public class ItemFactory : MonoBehaviour
{
    //아이템을 하나 만들때 마다 늘릴 아이템 갯수
    static int itemCount = 0;

    public static GameObject MakeItem(ItemIDCode itemIDCode)
    {
        GameObject obj = new GameObject();      //새로운 오브젝트 만들고
        Item item = obj.AddComponent<Item>();   //아무튼 아이템 컴포넌트 추가, 인스턴스를 만들어야 다른 변수값을 바꿀수 있어 item인스턴스를 만든듯

        item.data = GameManager.Instance.ItemManager[itemIDCode];   //추가된 컴포넌트의 데이터는 ItemIdCode의 데이터에 따라간다.
        obj.name = $"{item.data.name}_{itemCount}";                 //이름 설정
        obj.layer = LayerMask.NameToLayer("Item");                  //레이어 설정
        SphereCollider sphereCollider = obj.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.5f;
        sphereCollider.isTrigger = true;
        itemCount++;    //현재 아이템 갯수 한개 추가

        //강사님은 여기에 미니맵표시 추가를 함, 나중에 미니맵 만들면 추가 요망

        return obj;
    }

    //숫자타입으로 온애를 enum타입으로 형변환해서 만들어주는 함수
    public static GameObject MakeItem(uint id)
    {
        GameObject obj = MakeItem((ItemIDCode)id);  
        return obj;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //아이템 만들시 위치값을 미세하게 다르게 나오도록 조정
    // bool randomNoise = false 는 만약에 파라미터로 randomNoise만 따로 안적으면 false로 취급한다는 뜻이다. ex) MakeItem(HP_Potion, new(1,1,0))이면 ranndomNoise는 false로 들어감
    public static GameObject MakeItem(ItemIDCode itemIDCode, Vector3 itemPosition, bool randomNoise = false)   
    {
        GameObject obj = MakeItem(itemIDCode);

        if(randomNoise)
        {
            Vector2 noise = Random.insideUnitCircle * 0.5f;
            itemPosition.x += noise.x;
            itemPosition.y += noise.y;
        }

        obj.transform.position = itemPosition;

        return obj;
    }
    //숫자 id 버전
    public static GameObject MakeItem(uint id, Vector3 itemPosition, bool randomNoise = false)
    {
        return (MakeItem((ItemIDCode)id, itemPosition, randomNoise));
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    //얘는 한번에 여러개의 아이템을 만드는 코드로 리턴값이 void임
    public static void MakeItem(ItemIDCode itemIDCode, Vector3 itemPosition, uint count)
    {
        //GameObject obj = MakeItem(itemIDCode);

        for(int i=0; i < count; i++)
        {
            MakeItem(itemIDCode, itemPosition, true);
        } 
    }

    public static void MakeItem(uint id, Vector3 itemPosition, uint count)
    {
        //GameObject obj = MakeItem(itemIDCode);

        MakeItem((ItemIDCode)id, itemPosition, count);
    }







}
