using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayButtonView : ButtonViewBase
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    protected override void OnClickButton()
    {
        LevelSettingsSO settings = GameManager.Instance.levelSettings;
        if ( 1 < settings.Condition1Value  && settings.Condition1Value < settings.Condition2Value && settings.Condition2Value < settings.Condition3Value)
        {
            if (text != null) text.text = "";
            GameManager.Instance.StartLevel();
            panel.gameObject.SetActive(false);
        }
        else
        {
            if (text != null) text.text = "Conditions must be in ascending order.";
        }
    }
}
