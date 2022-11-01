using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ����� �޼ҵ��� ����(MakeItem)
public class ItemFactory : MonoBehaviour
{
    //�������� �ϳ� ���鶧 ���� �ø� ������ ����
    static int itemCount = 0;

    public static GameObject MakeItem(ItemIDCode itemIDCode)
    {
        GameObject obj = new GameObject();      //���ο� ������Ʈ �����
        Item item = obj.AddComponent<Item>();   //�ƹ�ư ������ ������Ʈ �߰�, �ν��Ͻ��� ������ �ٸ� �������� �ٲܼ� �־� item�ν��Ͻ��� �����

        item.data = GameManager.Instance.ItemManager[itemIDCode];   //�߰��� ������Ʈ�� �����ʹ� ItemIdCode�� �����Ϳ� ���󰣴�.
        obj.name = $"{item.data.name}_{itemCount}";                 //�̸� ����
        obj.layer = LayerMask.NameToLayer("Item");                  //���̾� ����
        SphereCollider sphereCollider = obj.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.5f;
        sphereCollider.isTrigger = true;
        itemCount++;    //���� ������ ���� �Ѱ� �߰�

        //������� ���⿡ �̴ϸ�ǥ�� �߰��� ��, ���߿� �̴ϸ� ����� �߰� ���

        return obj;
    }

    //����Ÿ������ �¾ָ� enumŸ������ ����ȯ�ؼ� ������ִ� �Լ�
    public static GameObject MakeItem(uint id)
    {
        GameObject obj = MakeItem((ItemIDCode)id);  
        return obj;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //������ ����� ��ġ���� �̼��ϰ� �ٸ��� �������� ����
    // bool randomNoise = false �� ���࿡ �Ķ���ͷ� randomNoise�� ���� �������� false�� ����Ѵٴ� ���̴�. ex) MakeItem(HP_Potion, new(1,1,0))�̸� ranndomNoise�� false�� ��
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
    //���� id ����
    public static GameObject MakeItem(uint id, Vector3 itemPosition, bool randomNoise = false)
    {
        return (MakeItem((ItemIDCode)id, itemPosition, randomNoise));
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    //��� �ѹ��� �������� �������� ����� �ڵ�� ���ϰ��� void��
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
