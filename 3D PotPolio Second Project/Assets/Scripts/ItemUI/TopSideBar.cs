using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TopSideBar : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Button topSideBarButton;
    RectTransform parentRectTransform;

    private void Awake()
    {
        //topSideBarButton = GetComponent<Button>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
    }


    public void OnBeginDrag(PointerEventData eventData)  //������ �� �ش� ��ġ�� pivot������ �����ϴ� �Լ�
    {
        float absoluteMinPosition_x = (parentRectTransform.position.x - parentRectTransform.rect.width * (parentRectTransform.pivot.x));
        float absoluteMinPosition_y = (parentRectTransform.position.y - parentRectTransform.rect.height * (parentRectTransform.pivot.y));

        parentRectTransform.pivot = new Vector2((eventData.position.x - absoluteMinPosition_x) / parentRectTransform.rect.width,
                                                    (eventData.position.y - absoluteMinPosition_y) / parentRectTransform.rect.height);

        parentRectTransform.position = eventData.position;

        //transform.parent.transform.position = eventData.position;   
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)  //���콺�� ������ ������ ȣ��
    {
        parentRectTransform.position = eventData.position;
    }
}
