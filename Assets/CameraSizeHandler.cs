using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeHandler : MonoBehaviour
{
    [SerializeField] private Camera cameraMain;

    public void AdjustCameraSize()
    {
         float width;
         float height;
         GameManager.Instance.GridManager.GetGridDimensions(out width, out height);
         cameraMain.orthographicSize = Mathf.Max(Mathf.Max((width / 2)+2, (height / 2)+2), 11);
    }
}
