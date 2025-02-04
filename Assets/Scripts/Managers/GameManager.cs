using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] public LevelSettingsSO levelSettings;
    [SerializeField] public GridManager GridManager;
    [SerializeField] public CameraSizeHandler CameraSizeHandler;
    [SerializeField] public TopPanelView TopPanelView;
    [SerializeField] public GameEndPanelView GameEndPanel;
    
    [Header("Pools")]
    [SerializeField] public ObjectPool BlockPool;
    [SerializeField] public ObjectPool TilePool;

    public int MoveCount;
    public int[] MissionValues = new int[6];
    public bool isClickOn;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isClickOn)
                return;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.OnClick();
            }
        }
    }

    public void StartLevel()
    {
        MoveCount = levelSettings.MoveCount;
        MissionValues = new int[levelSettings.MissionValues.Length];
        Array.Copy(levelSettings.MissionValues, MissionValues, levelSettings.MissionValues.Length);
        GridManager.Initialize();
        CameraSizeHandler.AdjustCameraSize();
        TopPanelView.Initialize();
        isClickOn = true;
    }

    public void DecreaseMoveCount()
    {
        if(levelSettings.MoveCount == 0 || levelSettings.MissionValues.All(x => x == 0))
            return;
        MoveCount--;
        TopPanelView.UpdateView();
        if (MoveCount <= 0)
        {
            GameEnd(false);
        }
    }

    public void UpdateMissionValues(int blockColorNumber)
    {
        if (levelSettings.MissionValues[blockColorNumber] == 0 || MissionValues[blockColorNumber] == 0)
        {
            return;
        }
        MissionValues[blockColorNumber]--;
        TopPanelView.UpdateView();
        if (MissionValues.Count(i => i > 0) == 0)
        {
            GameEnd(true);
        }
    }
    
    private void GameEnd(bool isWin)
    {
        isClickOn =false;
        GameEndPanel.Initialize(isWin);
        GameEndPanel.gameObject.SetActive(true);
    }
}
