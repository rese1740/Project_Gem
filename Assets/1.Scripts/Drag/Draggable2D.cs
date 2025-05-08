using System.Collections;
using UnityEngine;

public class Draggable2D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private Transform originalParent;
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

        // 원래 부모를 저장해놓고, 부모에서 떼어냄
        originalParent = transform.parent;
        transform.SetParent(null);
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
        Collider2D hit = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("DropZone", "Trash"));

        if (hit != null)
        {
            string layerName = LayerMask.LayerToName(hit.gameObject.layer);
            Gem slotGem = hit.GetComponentInChildren<Gem>();

            if (layerName == "Trash")
            {
                Debug.Log("쓰레기통에 버림");
                Destroy(gameObject);
            }
            else if (layerName == "DropZone")
            {
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
                    // 원래 위치로 되돌리기
                    transform.position = originalPosition;
                    transform.SetParent(originalParent);  
                }
            }
        }
        else
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);  
        }
    }

}
