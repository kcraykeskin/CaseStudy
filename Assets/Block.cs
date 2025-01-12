using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public int colorNumber;
    public Vector2Int gridPosition;
    public bool isFalling;
    
    public void Init(Vector2Int gPostion, int cNumber)
    {
        ChangeGridPosition(gPostion);
        ChangeColor(cNumber);
    }

    private void ChangeColor(int cNumber)
    {
        colorNumber = cNumber;
        sprite = GridManager.Instance.selectedColorSprites[colorNumber].sprites[0];
        spriteRenderer.sprite = sprite;
    }

    private void ChangeSpriteCondition(int i)
    {
        spriteRenderer.sprite = GridManager.Instance.selectedColorSprites[colorNumber].sprites[i];
    }

    private void ChangeGridPosition(Vector2Int gPostion)
    {
        gridPosition = gPostion;
        spriteRenderer.sortingOrder = 10 + gridPosition.y;
    }
}


