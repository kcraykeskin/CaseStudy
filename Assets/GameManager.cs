using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [SerializeField] public GridManager GridManager;
    [SerializeField] public CameraSizeHandler CameraSizeHandler;
    
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
