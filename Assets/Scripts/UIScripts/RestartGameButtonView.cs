using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameButtonView : ButtonViewBase
{
    [SerializeField] private GameObject panel;
    protected override void OnClickButton()
    {
        GameManager.Instance.GridManager.ResetBoard();
        panel.gameObject.SetActive(true);
    }
}
