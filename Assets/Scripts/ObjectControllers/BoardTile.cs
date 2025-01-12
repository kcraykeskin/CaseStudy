using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public Vector2Int inGridPosition;

    public GameObject straightBorder;
    public GameObject curvyBorder;
    public void Init(Vector2Int gPosition)
    {
        GetComponent<SpriteRenderer>().size = new Vector2(2.5f, 2.9f);
        inGridPosition =gPosition; 
        CreateBorders();
    }

    private void CreateBorders()
    {
        Vector2Int levelSize = GridManager.Instance.levelSettings.levelSize;

        if (inGridPosition.x == 0)
        {
            Instantiate(straightBorder.GetComponent<SpriteRenderer>(),
                    transform.position + new Vector3(-1.3f, 0, 0), Quaternion.Euler(0, 0, 0), transform).size =
                new Vector2(0.2f, 2.5f);
            if (inGridPosition.y == 0)
            {
                Instantiate(curvyBorder.GetComponent<SpriteRenderer>(),
                    transform.position + new Vector3(-1.19f, -1.39f, 0), Quaternion.Euler(0, 0, 90), transform);
            }
        }
        if (inGridPosition.y == 0)
        {
            Instantiate(straightBorder.GetComponent<SpriteRenderer>(),
                    transform.position + new Vector3(0, -1.5f, 0), Quaternion.Euler(0, 0, 90), transform).size =
                new Vector2(0.2f, 2.2f);
            if (inGridPosition.x == levelSize.x - 1)
            {
                Instantiate(curvyBorder.GetComponent<SpriteRenderer>(),
                    transform.position + new Vector3(1.19f, -1.39f, 0), Quaternion.Euler(0, 0, 180), transform);
            }
        }
        if (inGridPosition.x == levelSize.x - 1)
        {
            Instantiate(straightBorder.GetComponent<SpriteRenderer>(), 
                transform.position + new Vector3(1.3f, 0, 0), Quaternion.Euler(0, 0, 0), transform).size = 
                new Vector2(0.2f, 2.5f);
            if (inGridPosition.y == levelSize.y - 1)
            {
                Instantiate(curvyBorder.GetComponent<SpriteRenderer>(),
                    transform.position + new Vector3(1.19f, 1.39f, 0), Quaternion.Euler(0, 0, 270), transform);
            }
        }
        if (inGridPosition.y == levelSize.y - 1)
        {
            Instantiate(straightBorder.GetComponent<SpriteRenderer>(), 
                transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(0, 0, 90), transform).size = 
                new Vector2(0.2f, 2.2f);
            if (inGridPosition.x == 0)
            {
                Instantiate(curvyBorder.GetComponent<SpriteRenderer>(),
                    transform.position + new Vector3(-1.19f, 1.39f, 0), Quaternion.Euler(0, 0, 0), transform);
            }
        }
    }
}

