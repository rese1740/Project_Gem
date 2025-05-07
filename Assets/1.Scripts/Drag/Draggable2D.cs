using System.Collections;
using UnityEngine;

public class Draggable2D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private Vector3 mouseDownPos;

    private Gem myGem;

    public float dragThreshold = 0.1f; // 드래그 인식 임계값

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

        // 드래그 후 놓았을 때 처리
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
                    Debug.Log("아이템 랭크가 상승했습니다!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("병합 실패: 아이템 ID 또는 랭크가 다릅니다.");
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
