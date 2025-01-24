using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    [SerializeField] public GridManager GridManager;
    [SerializeField] public CameraSizeHandler CameraSizeHandler;
    [SerializeField] public ObjectPool BlockPool;
    [SerializeField] public ObjectPool TilePool;
    [SerializeField] public ObjectPool FramePool;
    
    private void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        GridManager.Initialize();
        CameraSizeHandler.AdjustCameraSize();
    }
}
