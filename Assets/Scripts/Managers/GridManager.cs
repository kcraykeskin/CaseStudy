using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [Header("Poola Alınacaklar")]
    [SerializeField] private GameObject boardTilePrefab;
    [SerializeField] private GameObject blockPrefab;

    [Header("GameSettingse Alınacaklar")] 
    [SerializeField] private float spriteHalfWidth = 2.2f;
    [SerializeField] public List<ColorSprites> allColorSprites;

    [Header("Kalacaklar")] 
    public LevelSettingsSO levelSettings;
    public List<ColorSprites> selectedColorSprites;
    public Transform boardPivot;
    public Vector2[] gridPositions; 
    public BoardTile[] boardTiles;
    public Block[] blocks;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {   
        selectedColorSprites = allColorSprites.OrderBy(x => Random.value)
            .Take(levelSettings.numberOfColors)
            .ToList();
        CreateGridPositions(levelSettings.levelSize.x, levelSettings.levelSize.y);
        CreateBoard(levelSettings.levelSize.x, levelSettings.levelSize.y);
        blocks = new Block[levelSettings.levelSize.x * levelSettings.levelSize.y];
        FillTheGrid(levelSettings.levelSize.x, levelSettings.levelSize.y);
    }
    
    private void CreateGridPositions(int xSize, int ySize)
    {
        gridPositions = new Vector2[xSize * ySize];
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                gridPositions[x + (y * xSize)] = (Vector2)boardPivot.position +new Vector2(x * spriteHalfWidth, y * spriteHalfWidth);
            }
        }
    }

    private void CreateBoard(int xSize, int ySize)
    {
        boardTiles = new BoardTile[xSize * ySize];
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                var boardTile = Instantiate(boardTilePrefab.GetComponent<BoardTile>(), gridPositions[x + (y * xSize)], Quaternion.identity, boardPivot);
                Instantiate(blockPrefab, gridPositions[x + (y * xSize)], Quaternion.identity);
                boardTile.Init(new Vector2Int(x,y));
                boardTiles[x + (y * xSize)] = boardTile;
            }
        }
    }

    public void FillTheGrid(int xSize, int ySize)
    {
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                var block = Instantiate(blockPrefab.GetComponent<Block>(), gridPositions[x + (y * xSize)], Quaternion.identity);
                int rand = Random.Range(0, levelSettings.numberOfColors);
                print(rand);
                block.Init(new Vector2Int(x, y), rand);
                blocks[x + (y * xSize)] = block;
            }
        }
    }
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
