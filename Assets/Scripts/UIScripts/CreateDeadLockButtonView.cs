using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDeadLockButtonView : ButtonViewBase
{
    protected override void OnClickButton()
    {
        GameManager.Instance.GridManager.CreateDeadLock();
    }
}
