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
	/// ���콺 ����Ʈ�� ���� ������ ���� ���� ���η� �� �� 1ȸ ȣ��
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData)
	{
		// ������ ������ ������ ��������� ����
		image.color = Color.yellow;
	}

	/// <summary>
	/// ���콺 ����Ʈ�� ���� ������ ���� ������ �������� �� 1ȸ ȣ��
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		// ������ ������ ������ �Ͼ������ ����
		image.color = Color.white;
	}

    public void OnDrop(PointerEventData eventData)
    {
        // ������ ��� ���� ���� ���
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            // �巡�׵� �����۰� ���� ���Կ� �ִ� �������� ItemID ��
            JamData draggedItemData = eventData.pointerDrag.GetComponent<Jam>().itemData; // �巡�׵� �������� ItemData
            Jam slotJam = transform.GetComponentInChildren<Jam>();  // ���Կ� �ִ� Jam ������Ʈ�� ������

            // ���Կ� �������� ������
            if (slotJam != null)
            {
                JamData slotItemData = slotJam.itemData; // ���Կ� �ִ� �������� ItemData

                if (draggedItemData != null && slotItemData != null)
                {
                    // ������ ItemID�� ��� ��ũ�� �÷���
                    if (draggedItemData.itemID == slotItemData.itemID)
                    {
                        // ��ũ �ø���
                        slotJam.LevelUp();  // ���� �������� ��ũ�� �÷���
                        Debug.Log("������ ��ũ�� ����߽��ϴ�!");

                        // �巡�׵� �������� ����(��ü���� �ʰ� �ø��⸸ �ϹǷ�)
                        Destroy(eventData.pointerDrag);
                    }
                    else
                    {
                        // ������ ID�� �ٸ��� �巡�׵� �������� ���Կ� �߰�
                        eventData.pointerDrag.transform.SetParent(transform);
                        eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
                    }
                }
            }
            else
            {
                // ���Կ� ���� �������� ������ �׳� �巡�׵� �������� ���Կ� �߰�
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            }
        }
        else
        {
            Debug.Log("�� ������ �̹� ��� ���Դϴ�!");
        }
    }


}

