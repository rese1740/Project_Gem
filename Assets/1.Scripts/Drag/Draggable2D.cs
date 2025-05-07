using System.Collections;
using UnityEngine;

public class Draggable2D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private Vector3 mouseDownPos;

    private Gem myGem;

    public float dragThreshold = 0.1f; // �巡�� �ν� �Ӱ谪

    void Start()
    {
        myGem = GetComponent<Gem>();
    }

    void OnMouseDown()
    {
        isDragging = false;
        mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseDownPos.x, mouseDownPos.y, transform.position.z);
        originalPosition = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dragDistance = Vector3.Distance(mouseDownPos, mousePos);

        if (dragDistance > dragThreshold)
        {
            isDragging = true;
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
        }
    }

    void OnMouseUp()
    {
        if (!isDragging)
        {
            return;
        }

        // �巡�� �� ������ �� ó��
        Collider2D hit = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("DropZone"));

        if (hit != null)
        {
            Gem slotGem = hit.GetComponentInChildren<Gem>();

            if (slotGem == null)
            {
                transform.position = hit.transform.position;
                transform.SetParent(hit.transform);
            }
            else
            {
                if (myGem != null && slotGem != null && myGem.itemData.itemID == slotGem.itemData.itemID && myGem.currentRank == slotGem.currentRank)
                {
                    slotGem.LevelUp();
                    Debug.Log("������ ��ũ�� ����߽��ϴ�!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("���� ����: ������ ID �Ǵ� ��ũ�� �ٸ��ϴ�.");
                    transform.position = originalPosition;
                }
            }
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}
