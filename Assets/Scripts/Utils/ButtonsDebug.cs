using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsDebug : MonoBehaviour
{
    public void DebugButtons()
    {
        Debug.Log("Buttons was clicked");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Clicked on: {hit.collider.gameObject.name}");
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log($"Clicked on: {hit.collider.gameObject.name}");
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    PointerEventData eventData = new PointerEventData(EventSystem.current);
        //    eventData.position = Input.mousePosition;
        //
        //    List<RaycastResult> results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(eventData, results);
        //
        //    foreach (var result in results)
        //    {
        //        Debug.Log($"Clicked on UI: {result.gameObject.name}");
        //    }
        //}
    }
}
