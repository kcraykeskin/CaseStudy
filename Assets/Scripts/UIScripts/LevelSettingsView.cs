using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettingsView : MonoBehaviour
{
    private LevelSettingsSO settings;

    [Header("UI Elements")]
    public Slider numberOfRowsSlider;
    public Slider numberOfColumnsSlider;
    public Slider numberOfColorsSlider;
    
    public TextMeshProUGUI numberOfRowsText;
    public TextMeshProUGUI numberOfColumnsText;
    public TextMeshProUGUI numberOfColorsText;

    public TMP_InputField condition1Input;
    public TMP_InputField condition2Input;
    public TMP_InputField condition3Input;
    
    public TMP_InputField[] MissionInputs = new TMP_InputField[6];
    public TMP_InputField MoveCountInput;

    private void Start()
    {
        settings = GameManager.Instance.levelSettings;
        numberOfColumnsSlider.value = settings.numberOfColumns;
        numberOfRowsSlider.value = settings.numberOfRows;
        numberOfColorsSlider.value = settings.numberOfColors;
        
        numberOfRowsText.text = settings.numberOfRows.ToString();
        numberOfColumnsText.text =settings.numberOfColumns.ToString();
        numberOfColorsText.text = settings.numberOfColors.ToString();

        condition1Input.text = settings.Condition1Value.ToString();
        condition2Input.text = settings.Condition2Value.ToString();
        condition3Input.text = settings.Condition3Value.ToString();

        numberOfColumnsSlider.onValueChanged.AddListener(value => settings.numberOfColumns = Mathf.RoundToInt(value));
        numberOfRowsSlider.onValueChanged.AddListener(value => settings.numberOfRows = Mathf.RoundToInt(value));
        numberOfColorsSlider.onValueChanged.AddListener(value => settings.numberOfColors = Mathf.RoundToInt(value));
        
        numberOfColumnsSlider.onValueChanged.AddListener(value => numberOfColumnsText.text = Mathf.RoundToInt(value).ToString());
        numberOfRowsSlider.onValueChanged.AddListener(value => numberOfRowsText.text = Mathf.RoundToInt(value).ToString());
        numberOfColorsSlider.onValueChanged.AddListener(value => numberOfColorsText.text = Mathf.RoundToInt(value).ToString());

        condition1Input.onEndEdit.AddListener(value => int.TryParse(value, out settings.Condition1Value));
        condition2Input.onEndEdit.AddListener(value => int.TryParse(value, out settings.Condition2Value));
        condition3Input.onEndEdit.AddListener(value => int.TryParse(value, out settings.Condition3Value));
        
         numberOfColorsSlider.onValueChanged.AddListener(UpdateMissionValues);
        
         for (int i = 0; i < 6; i++)
         {
             int index = i;
             MissionInputs[index].text = settings.MissionValues[index].ToString();
             MissionInputs[index].onEndEdit.AddListener(value =>
             {
                 int.TryParse(value, out settings.MissionValues[index]);
             });
         }


        
        MoveCountInput.text = settings.MoveCount.ToString();
        MoveCountInput.onEndEdit.AddListener(value => int.TryParse(value, out settings.MoveCount));
        UpdateMissionValues(settings.numberOfColors);
    }
    
    private void UpdateMissionValues(float a )
    {
        for (int i = 0; i < MissionInputs.Length; i++)
        {
            if (i < a)
            {
                MissionInputs[i].gameObject.SetActive(true);
            }
            else
            {
                MissionInputs[i].text = "0";
                settings.MissionValues[i] = 0;
                MissionInputs[i].gameObject.SetActive(false);
            }
        }
    }
}
