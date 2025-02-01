using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloseButtonView : ButtonViewBase
{
    [SerializeField] private GameObject panelToClose;
    protected override void OnClickButton()
    {
        panelToClose.SetActive(false);
    }
}
