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

        Collider2D hit = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("DropZone", "Trash"));
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        Collider2D hit = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("DropZone", "Trash"));

        if (hit != null)
        {
            string layerName = LayerMask.LayerToName(hit.gameObject.layer);

            if (layerName == "Trash")
            {
                Debug.Log("쓰레기통에 버림");
                Destroy(gameObject);
            }
            else if (layerName == "DropZone")
            {
                Gem slotGem = hit.GetComponentInChildren<Gem>();

                if (slotGem == null)
                {
                    transform.position = hit.transform.position;
                    transform.SetParent(hit.transform);
                }
                else if (myGem != null &&
                         myGem.itemData.itemID == slotGem.itemData.itemID &&
                         myGem.currentRank == slotGem.currentRank)
                {
                    slotGem.LevelUp();
                    Debug.Log("합성 성공!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("합성 실패");
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
