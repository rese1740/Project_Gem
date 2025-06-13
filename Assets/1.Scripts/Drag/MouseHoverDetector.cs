using UnityEngine;

public class MouseHoverDetector : MonoBehaviour
{
    private SpriteRenderer lastRenderer;
    private Color origincolor;

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, LayerMask.GetMask("DropZone", "Trash"));

        if (hit != null)
        {
            SpriteRenderer sr = hit.GetComponent<SpriteRenderer>();

            // 이전에 바꿨던 오브젝트의 색상 복구
            if (lastRenderer != null && lastRenderer != sr)
            {
                Color color = lastRenderer.color;
                color.a = 10f;
                origincolor = color;
                lastRenderer.color = origincolor;
            }

            if (sr != null)
            {
                string layerName = LayerMask.LayerToName(hit.gameObject.layer);

                if (layerName == "Trash")
                    sr.color = Color.red;
                else if (layerName == "DropZone")
                    sr.color = Color.yellow;

                lastRenderer = sr;
            }
        }
        else
        {
            if (lastRenderer != null)
            {
                origincolor = Color.white;
                origincolor.a = 0.1f;
                lastRenderer.color = origincolor;
                lastRenderer = null;
            }
        }
    }
}
