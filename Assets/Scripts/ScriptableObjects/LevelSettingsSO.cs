using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "LevelSettings")]
public class LevelSettingsSO : ScriptableObject
{
    public Vector2Int levelSize;
    public int numberOfColors;
    
    public int Condition1Value;
    public int Condition2Value;
    public int Condition3Value;
}
