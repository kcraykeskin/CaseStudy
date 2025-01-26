using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseThenOpenPanelButtonView : ButtonViewBase
{
    [SerializeField] private GameObject panelToClose;
    [SerializeField] private GameObject panelToOpen;
    protected override void OnClickButton()
    {
        panelToOpen.SetActive(true);
        panelToClose.SetActive(false);
    }
}
