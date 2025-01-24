using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour, IClickable
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public int colorNumber;
    public Vector2Int gridPosition;
    public bool isFalling;
    
    [SerializeField] public Animator animator;
    private Tween fallTween;
    
    public void Init(Vector2Int gPostion, int cNumber)
    {
        ChangeGridPosition(gPostion);
        ChangeColor(cNumber);
    }

    private void ChangeColor(int cNumber)
    {
        colorNumber = cNumber;
        sprite = GameManager.Instance.GridManager.selectedColorSprites[colorNumber].sprites[0];
        spriteRenderer.sprite = sprite;
    }

    public void ChangeSpriteCondition(int i)
    {
        spriteRenderer.sprite = GameManager.Instance.GridManager.selectedColorSprites[colorNumber].sprites[i];
    }

    public void ChangeGridPosition(Vector2Int gPostion)
    {
        gridPosition = gPostion;
        spriteRenderer.sortingOrder = 10 + gridPosition.y;
    }

    public void StartMatching()
    {
        GameManager.Instance.GridManager.TempMatchList.Clear();
        GameManager.Instance.GridManager.TempMatchList.Add(this);
        CheckMatchesAround(colorNumber);
    }

    public void CheckMatchesAround(int i)
    {
        foreach (var block in GameManager.Instance.GridManager.GetBlocksAround(this))
        {
            if (block && !block.isFalling)
            {
                if ((colorNumber == block.colorNumber) && !GameManager.Instance.GridManager.TempMatchList.Contains(block))
                {
                    GameManager.Instance.GridManager.TempMatchList.Add(block);
                    block.CheckMatchesAround(colorNumber);
                }
            }
        }
    }

    public void OnClick()
    {
        Match clickedGroup= (GameManager.Instance.GridManager.GetMatchContaining(this));
        if (clickedGroup.MatchedBlocks.Count >= 2)
        {
            GameManager.Instance.GridManager.BlastMatch(clickedGroup);
        }
        else
        {
            animator.SetTrigger("WrongClickShake");
        }
    }

    public void Blast()
    {
        GameManager.Instance.BlockPool.Return(gameObject);
    }
    
    public void MoveTo(Vector3 targetPosition)
    {
        if (fallTween != null && fallTween.IsActive())
        {
            fallTween.Kill();
        }

        fallTween = transform.DOMove(targetPosition, Mathf.Abs(Vector3.Distance(targetPosition, transform.position))/14)
            .SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                animator.SetTrigger("GroundHit");
                fallTween = null;
            }); 
    }
}


