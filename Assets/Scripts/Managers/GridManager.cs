using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [Header("GameSettingse Alınacaklar")] 
    [SerializeField] private float spriteHalfWidth = 2.2f;
    [SerializeField] public List<ColorSprites> allColorSprites;

    [Header("Kalacaklar")] 
    private LevelSettingsSO levelSettings;
    public List<ColorSprites> selectedColorSprites;
    public Transform boardPivot;
    public Transform blockContainer;
    public Transform boardTileContainer;
    public Vector2[] gridPositions; 
    public BoardTile[] boardTiles;
    public Block[] blocks;

    [Header("MatchList")] 
    public List<Block> TempMatchList;
    public List<Match> MatchList;


    public int xSize;
    public int ySize;
    
    public void Initialize()
    {
        levelSettings = GameManager.Instance.levelSettings;
        xSize = levelSettings.levelSize.x;
        ySize = levelSettings.levelSize.y;
        selectedColorSprites = allColorSprites
            .OrderBy(_ => Guid.NewGuid())
            .Take(levelSettings.numberOfColors)
            .ToList();
        CreateGridPositions();
        CreateBoard();
        blocks = new Block[levelSettings.levelSize.x * levelSettings.levelSize.y];
        FillTheGrid();
        FindMatches();
    }
    
   private void CreateGridPositions()
   {
       gridPositions = new Vector2[xSize * ySize];
       float offsetX = (xSize - 1) * spriteHalfWidth / 2f;
       float offsetY = (ySize - 1) * spriteHalfWidth / 2f;
       Vector2 offset = new Vector2(-offsetX, -offsetY);
   
       for (int y = 0; y < ySize; y++)
       {
           for (int x = 0; x < xSize; x++)
           {
               gridPositions[x + (y * xSize)] = 
                   (Vector2)boardPivot.position + offset + new Vector2(x * spriteHalfWidth, y * spriteHalfWidth);
           }
       }
   }

    private void CreateBoard()
    {
        boardTiles = new BoardTile[xSize * ySize];
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                var boardTile = GameManager.Instance.TilePool.Get(gridPositions[x + (y * xSize)], Quaternion.identity, boardTileContainer).GetComponent<BoardTile>();
                boardTile.Init(new Vector2Int(x,y));
                boardTiles[x + (y * xSize)] = boardTile;
            }
        }
    }

    public void FillTheGrid()
    {
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                var block = GameManager.Instance.BlockPool.Get(gridPositions[x + (y * xSize)], Quaternion.identity, blockContainer).GetComponent<Block>();
                int rand = Random.Range(0, levelSettings.numberOfColors);
                block.name = $"Block{x} {y}";
                block.Init(new Vector2Int(x, y), rand);
                blocks[x + (y * xSize)] = block;
            }
        }
    }

    public void FindMatches()
    {
        
        MatchList.Clear();
        TempMatchList.Clear();
    
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                if (blocks[x + (y * xSize)] != null && 
                    MatchList.SelectMany(i => i.MatchedBlocks).All(i => i != blocks[x + (y * xSize)]))
                {
                    blocks[x + (y * xSize)].StartMatching();
                
                    MatchList.Add(new Match { MatchedBlocks = new List<Block>(TempMatchList) });
                    TempMatchList.Clear();
                }
            }
        }

        UpdateConditionSprites(xSize, ySize);
    }

    private void UpdateConditionSprites(int xSize, int ySize)
    {
        foreach (var match in MatchList)
        {
            int val = match.MatchedBlocks.Count;
            foreach (var block in match.MatchedBlocks)
            {
                if (val <= levelSettings.Condition1Value)
                {
                    block.ChangeSpriteCondition(0);
                }
                else if (levelSettings.Condition1Value < val && val <= levelSettings.Condition2Value)
                {
                    block.ChangeSpriteCondition(1);
                }
                else if (levelSettings.Condition2Value < val && val <= levelSettings.Condition3Value)
                {
                    block.ChangeSpriteCondition(2);
                }
                else if (val > levelSettings.Condition3Value)
                {
                    block.ChangeSpriteCondition(3);
                }
            }
        }
    }


    public List<Block> GetBlocksAround(Block block)
    {
        List<Block> tempList = new List<Block>();
        Vector2Int pos = block.gridPosition;
        int width = levelSettings.levelSize.x;
        int height = levelSettings.levelSize.y;
        
        if (pos.x > 0 && blocks[(pos.x - 1) + (pos.y * width)])
        {
            tempList.Add(blocks[(pos.x - 1) + (pos.y * width)]);
        }
    
        if (pos.x < width - 1 && blocks[(pos.x + 1) + (pos.y * width)])
        {
            tempList.Add(blocks[(pos.x + 1) + (pos.y * width)]);
        }
    
        if (pos.y > 0 && blocks[pos.x + ((pos.y - 1) * width)])
        {
            tempList.Add(blocks[pos.x + ((pos.y - 1) * width)]);
        }
    
        if (pos.y < height - 1 && blocks[pos.x + ((pos.y + 1) * width)])
        {
            tempList.Add(blocks[pos.x + ((pos.y + 1) * width)]);
        }
    
        return tempList;
    }

    public Match GetMatchContaining(Block block)
    {
        return MatchList.FirstOrDefault(m => m.MatchedBlocks.Contains(block));
    }

    public void BlastMatch(Match clickedGroup)
    {
        foreach (Block block in clickedGroup.MatchedBlocks)
        {
            blocks[block.gridPosition.x + block.gridPosition.y * levelSettings.levelSize.x] = null;
            block.Blast();
        }
        ApplyGravity();
    }



    private void ApplyGravity()
    {
        for (int x = 0; x < levelSettings.levelSize.x; x++)
        {
            for (int y = 1; y < levelSettings.levelSize.y; y++)
            {
                if(!GetBlock(x,y))
                {
                    continue;
                }
                
                int tempBottom = -1;
                for (int b = y; b >= 0; b--)
                {
                    if ( blocks[x + b * levelSettings.levelSize.x] != null)
                    {
                        continue;
                    }
                    tempBottom = b;
                }
                if (tempBottom != -1 && blocks[x + tempBottom * levelSettings.levelSize.x] == null)
                {
                    Block movingBlock = GetBlock(x,y);
                    if (movingBlock)
                    {
                        blocks[x + tempBottom * levelSettings.levelSize.x] = blocks[x + y*levelSettings.levelSize.x];
                        blocks[x + tempBottom * levelSettings.levelSize.x].ChangeGridPosition(new Vector2Int(x, y: tempBottom));
                        blocks[x + tempBottom * levelSettings.levelSize.x].MoveTo(gridPositions[x + tempBottom * levelSettings.levelSize.x]);
                        blocks[x + y * levelSettings.levelSize.x] = null;
                    }
                }
            }
        }
        FillEmptyTiles();
        FindMatches();
    }

    private void FillEmptyTiles()
    {
        for (int x = 0; x < xSize; x++)
        {
            int emptyTileCount = 0;
            for (int y = 0; y < ySize; y++)
            {
                if (blocks[x + y * xSize] == null)
                {
                    emptyTileCount++;
                }
            }

            for (int i = 0; i < emptyTileCount; i++)
            {
                Vector2 topPos = new Vector2(gridPositions[x].x, (ySize/2+i+1) * spriteHalfWidth);
                var block =GameManager.Instance.BlockPool.Get(topPos, Quaternion.identity, blockContainer).GetComponent<Block>();
                int rand = Random.Range(0, levelSettings.numberOfColors);
                int tempY = ySize - (emptyTileCount - i);
                block.name = $"Block{x} {tempY}";
                block.Init(new Vector2Int(x, tempY), rand);
                blocks[x + (tempY * xSize)] = block;
                block.MoveTo(gridPositions[x + (tempY * xSize)]);
            }
        }
    }
    private Block GetBlock(int x, int y)
    {
        return blocks[x + y * levelSettings.levelSize.x];
    }
    
    public void GetGridDimensions(out float width, out float height)
    {
        width = (xSize - 1) * spriteHalfWidth * 2f;
        height = (ySize - 1) * spriteHalfWidth * 2f;
    }

    public void ResetBoard()
    {
        for (int i = 0; i < boardTiles.Length; i++)
        {
            GameManager.Instance.TilePool.Return(boardTiles[i].gameObject);
        }
        
        for (int i = 0; i < blocks.Length; i++)
        {
            GameManager.Instance.BlockPool.Return(blocks[i].gameObject);
        }
    }

    public void CreateDeadLock()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GetBlock(x, y).ChangeColor((x+y)%levelSettings.numberOfColors);
            }
        }
        FindMatches();
    }
}

[Serializable]
public class Match
{
    public List<Block> MatchedBlocks = new List<Block>();
}


[Serializable]
public class ColorSprites
{
    public BlockColor blockColor;
    public Sprite[] sprites;
} 
public enum BlockColor
{
    Blue,
    Green,
    Pink,
    Purple,
    Red,
    Yellow,
}
