using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

    public void Initialize(bool isWin)
    {
        image.color = isWin ? Color.white : Color.red;
        text.text = isWin ? "You Win!" : "You Lose!";
    }
}
