using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
	private	Image image;
	private	RectTransform rect;

	private void Awake()
	{
		image = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
	}


	public void OnPointerEnter(PointerEventData eventData)
	{
		// 아이템 슬롯의 색상을 노란색으로 변경
		image.color = Color.yellow;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		image.color = Color.white;
	}

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Jam slotJam = transform.GetComponentInChildren<Jam>();

            if (slotJam == null)  
            {
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            }
            else
            {
                Jam draggedItemJam = eventData.pointerDrag.GetComponent<Jam>(); 
                if (draggedItemJam != null)
                {
                    // 슬롯과 드래그된 아이템의 currentRank가 동일한지 비교
                    if (draggedItemJam.currentRank == slotJam.currentRank)
                    {
                        slotJam.LevelUp();  
                        Debug.Log("아이템 랭크가 상승했습니다!");

                        Destroy(eventData.pointerDrag);
                    }
                }
            }
        }
    }




}

