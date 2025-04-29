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
		// ������ ������ ������ ��������� ����
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
                    // ���԰� �巡�׵� �������� currentRank�� �������� ��
                    if (draggedItemJam.currentRank == slotJam.currentRank)
                    {
                        slotJam.LevelUp();  
                        Debug.Log("������ ��ũ�� ����߽��ϴ�!");

                        Destroy(eventData.pointerDrag);
                    }
                }
            }
        }
    }




}

