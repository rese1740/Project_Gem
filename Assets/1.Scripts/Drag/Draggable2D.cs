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

        // ���� �θ� �����س���, �θ𿡼� ���
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

        // �巡�� �� ������ �� ó��
        Collider2D hit = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("DropZone", "Trash"));

        if (hit != null)
        {
            string layerName = LayerMask.LayerToName(hit.gameObject.layer);
            Gem slotGem = hit.GetComponentInChildren<Gem>();

            if (layerName == "Trash")
            {
                Debug.Log("�������뿡 ����");
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
                    Debug.Log("�ռ� ����!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("�ռ� ����");
                    // ���� ��ġ�� �ǵ�����
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
