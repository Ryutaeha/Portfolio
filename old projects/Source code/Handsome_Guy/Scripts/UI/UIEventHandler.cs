using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    // 드래그 이벤트가 발생했을 때 실행될 액션입니다. PointerEventData를 매개변수로 받습니다.
    public Action<PointerEventData> DragAction;

    // 클릭 이벤트가 발생했을 때 실행될 액션입니다. PointerEventData를 매개변수로 받습니다.
    public Action<PointerEventData> ClickAction;

    // UI 요소가 드래그될 때 호출되는 메서드입니다.
    // eventData: 드래그 이벤트에 대한 정보를 담고 있는 PointerEventData 객체입니다.
    public void OnDrag(PointerEventData eventData)
    {
        // DragAction이 설정되어 있으면, 드래그 이벤트 데이터를 전달하면서 DragAction을 실행합니다.
        if (DragAction != null)
        {
            DragAction.Invoke(eventData);
        }
    }

    // UI 요소가 클릭될 때 호출되는 메서드입니다.
    // eventData: 클릭 이벤트에 대한 정보를 담고 있는 PointerEventData 객체입니다.
    public void OnPointerClick(PointerEventData eventData)
    {
        // ClickAction이 설정되어 있으면, 클릭 이벤트 데이터를 전달하면서 ClickAction을 실행합니다.
        if (ClickAction != null)
        {
            ClickAction.Invoke(eventData);
        }
    }
}

