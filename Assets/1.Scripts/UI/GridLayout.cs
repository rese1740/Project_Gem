using UnityEngine;

using UnityEngine;

public class GridLayout : MonoBehaviour
{
    public GameObject[] objectsToArrange;
    public int columns = 3;
    public float cellWidth = 1.5f;
    public float cellHeight = 1.5f;
    public Vector3 startPosition = Vector3.zero; 

    void Start()
    {
        for (int i = 0; i < objectsToArrange.Length; i++)
        {
            int row = i / columns;
            int col = i % columns;

            Vector3 newPos = new Vector3(col * cellWidth, -row * cellHeight, 0);
            objectsToArrange[i].transform.localPosition = startPosition + newPos;
        }
    }
}

