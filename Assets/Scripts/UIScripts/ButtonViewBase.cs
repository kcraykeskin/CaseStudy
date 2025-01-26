using UnityEngine;
using UnityEngine.UI;

public class ButtonViewBase : MonoBehaviour
{
    protected Button button;

    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickButton);
    }

    protected virtual void OnClickButton()
    {
        
    }
}