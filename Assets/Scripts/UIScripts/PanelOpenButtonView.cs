using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpenButtonView : ButtonViewBase
{
    [SerializeField] private GameObject panelToOpen;
    protected override void OnClickButton()
    {
        panelToOpen.SetActive(true);
    }
}
