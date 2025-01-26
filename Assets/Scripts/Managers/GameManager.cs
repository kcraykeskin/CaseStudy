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
    
    public LevelSettingsSO levelSettings;
    [SerializeField] public GridManager GridManager;
    [SerializeField] public CameraSizeHandler CameraSizeHandler;
    
    [Header("Pools")]
    [SerializeField] public ObjectPool BlockPool;
    [SerializeField] public ObjectPool TilePool;
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        GridManager.Initialize();
        CameraSizeHandler.AdjustCameraSize();
    }
}
