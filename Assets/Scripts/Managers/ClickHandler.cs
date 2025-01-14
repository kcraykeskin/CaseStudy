using UnityEngine;

public class ClickHandler : MonoBehaviour
{ void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.OnClick();
            }
        }
    }
}
