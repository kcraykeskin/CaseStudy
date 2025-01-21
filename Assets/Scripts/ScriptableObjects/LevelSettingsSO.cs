using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "LevelSettings")]
public class LevelSettingsSO : ScriptableObject
{
    [Range(2, 10)]
    public int numberOfRows = 2;

    [Range(2, 10)]
    public int numberOfColumns = 2;
    
    [Range(1, 6)]
    public int numberOfColors = 1;
    public Vector2Int levelSize => new Vector2Int(numberOfRows, numberOfColumns);
    
    public int Condition1Value;
    public int Condition2Value;
    public int Condition3Value;
}
