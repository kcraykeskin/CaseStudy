using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public Vector2Int inGridPosition;

    [SerializeField] private GameObject TopLine;
    [SerializeField] private GameObject BottomLine;
    [SerializeField] private GameObject RightLine;
    [SerializeField] private GameObject LeftLine;
    
    [SerializeField] private GameObject LeftTopCurve;
    [SerializeField] private GameObject LeftBottomCurve;
    [SerializeField] private GameObject RightTopCurve;
    [SerializeField] private GameObject RightBottomCurve;
    public void Init(Vector2Int gPosition)
    {
        GetComponent<SpriteRenderer>().size = new Vector2(2.5f, 2.9f);
        inGridPosition =gPosition; 
        CreateBorders();
    }

    private void CreateBorders()
    {
        Vector2Int levelSize = new Vector2Int(GameManager.Instance.GridManager.xSize, GameManager.Instance.GridManager.ySize);
        
        TopLine.SetActive(false);
        BottomLine.SetActive(false);
        RightLine.SetActive(false);
        LeftLine.SetActive(false);
        LeftTopCurve.SetActive(false);
        LeftBottomCurve.SetActive(false);
        RightTopCurve.SetActive(false);
        RightBottomCurve.SetActive(false);
        
        if (inGridPosition.x == 0)
        {
            LeftLine.SetActive(true);
            if (inGridPosition.y == 0)
            {
                LeftBottomCurve.SetActive(true);
            }
        }
        if (inGridPosition.y == 0)
        {
            BottomLine.SetActive(true);
            if (inGridPosition.x == levelSize.x - 1)
            {
                RightBottomCurve.SetActive(true);
            }
        }
        if (inGridPosition.x == levelSize.x - 1)
        {
           RightLine.SetActive(true);
            if (inGridPosition.y == levelSize.y - 1)
            {
                RightTopCurve.SetActive(true);
            }
        }
        if (inGridPosition.y == levelSize.y - 1)
        {
          TopLine.SetActive(true);
            if (inGridPosition.x == 0)
            {
                LeftTopCurve.SetActive(true);
            }
        }
    }
}

