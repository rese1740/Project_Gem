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

    void Start()
    {
        myGem = GetComponent<Gem>();
    }

    void OnMouseDown()
    {
        Debug.Log("드래그 시작");
        isDragging = true;
        mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseDownPos.x, mouseDownPos.y, transform.position.z);
        originalPosition = transform.position;

     
        originalParent = transform.parent;
        transform.SetParent(null);
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
    }

    void OnMouseUp()
    {
        Debug.Log("드래그 끝");
        float dist = Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), mouseDownPos);
        if (dist < 0.1f) // 클릭으로 판단
        {
            isDragging = false;
            return;
        }
        if (!isDragging)
        {
            return;
        }

        Collider2D hit = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("DropZone", "Trash"));

        if (hit != null)
        {
            string layerName = LayerMask.LayerToName(hit.gameObject.layer);
            Gem slotGem = hit.GetComponentInChildren<Gem>();

            if (layerName == "Trash")
            {
                SFXManager.Instance.PlaySFX("Trash_Sound");
                switch (myGem.currentRank) 
                {
                    case 1:
                        GameManager.Instance.gold += GameManager.Instance.maxGold * 0.05f;
                        break;
                    case 2:
                        GameManager.Instance.gold += GameManager.Instance.maxGold * 0.15f;
                        break;
                    case 3:
                        GameManager.Instance.gold += GameManager.Instance.maxGold * 0.3f;
                        break;
                    case 4:
                        GameManager.Instance.gold += GameManager.Instance.maxGold * 0.5f;
                        break;

                    default:
                        Debug.Log(1);
                        break;
                }

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
                         myGem.currentRank == slotGem.currentRank && myGem.itemData.rank < myGem.itemData.maxRank)
                {
                    slotGem.LevelUp();
                    Destroy(gameObject);
                }
                else
                {
                    Vector3 offset = new Vector3(0, 0, -1);
                    transform.position = originalPosition + offset;
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
