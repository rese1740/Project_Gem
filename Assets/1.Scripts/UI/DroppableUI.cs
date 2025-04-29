using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
	private	Image			image;
	private	RectTransform	rect;

	private void Awake()
	{
		image	= GetComponent<Image>();
		rect	= GetComponent<RectTransform>();
	}

	/// <summary>
	/// 마우스 포인트가 현재 아이템 슬롯 영역 내부로 들어갈 때 1회 호출
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData)
	{
		// 아이템 슬롯의 색상을 노란색으로 변경
		image.color = Color.yellow;
	}

	/// <summary>
	/// 마우스 포인트가 현재 아이템 슬롯 영역을 빠져나갈 때 1회 호출
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		// 아이템 슬롯의 색상을 하얀색으로 변경
		image.color = Color.white;
	}

    public void OnDrop(PointerEventData eventData)
    {
        // 슬롯이 비어 있을 때만 허용
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            // 드래그된 아이템과 현재 슬롯에 있는 아이템의 ItemID 비교
            JamData draggedItemData = eventData.pointerDrag.GetComponent<Jam>().itemData; // 드래그된 아이템의 ItemData
            Jam slotJam = transform.GetComponentInChildren<Jam>();  // 슬롯에 있는 Jam 컴포넌트를 가져옴

            // 슬롯에 아이템이 있으면
            if (slotJam != null)
            {
                JamData slotItemData = slotJam.itemData; // 슬롯에 있는 아이템의 ItemData

                if (draggedItemData != null && slotItemData != null)
                {
                    // 동일한 ItemID일 경우 랭크를 올려줌
                    if (draggedItemData.itemID == slotItemData.itemID)
                    {
                        // 랭크 올리기
                        slotJam.LevelUp();  // 기존 아이템의 랭크를 올려줌
                        Debug.Log("아이템 랭크가 상승했습니다!");

                        // 드래그된 아이템은 삭제(대체하지 않고 올리기만 하므로)
                        Destroy(eventData.pointerDrag);
                    }
                    else
                    {
                        // 아이템 ID가 다르면 드래그된 아이템을 슬롯에 추가
                        eventData.pointerDrag.transform.SetParent(transform);
                        eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
                    }
                }
            }
            else
            {
                // 슬롯에 기존 아이템이 없으면 그냥 드래그된 아이템을 슬롯에 추가
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            }
        }
        else
        {
            Debug.Log("이 슬롯은 이미 사용 중입니다!");
        }
    }


}

