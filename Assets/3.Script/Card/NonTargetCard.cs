/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonTargetCard : BaseCard, IBeginDragHandler, IDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isBattle)
            return;

        _cardHolder.isDrag = true;

        _cardHolder.selectedCard = this;

        // 이동 시 현재 이동하는 코루틴 정지
        ClearCoroutine();

        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isBattle)
            return;

        // 카드가 마우스 따라가게
        transform.position = eventData.position;
    }

    // 드랍할 때
    public void OnDrop(PointerEventData eventData)
    {
        if (!isBattle)
            return;

        _cardHolder.isDrag = false;

        // 사용가능(코스트 등등)이거나 사용범위에 있을 때만 사용
        // 사용 범위 y값이 300이상 -> 이는 해상도에 따라 바꾸는 로직이 필요
        if (eventData.position.y > 300f)
        {
            SetActiveRaycast(false);
            UseCardMove();
        }

        // 아니면 재정렬
        _cardHolder.Relocation();
        // 할거 다 하고 null처리
        _cardHolder.selectedCard = null;
    }

    protected override bool Use()
    {
        bool isUsable = base.Use();

        if(isUsable)
        {
            BattleManager.Instance.Player.ShieldAmount += 5;
        }

        return isUsable;
    }
}
*/