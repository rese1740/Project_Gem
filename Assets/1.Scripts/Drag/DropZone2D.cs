using UnityEngine;

public class DropZone2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Draggable"))
        {
            Debug.Log("��� ������ ������ ����!");
        }
    }
}
