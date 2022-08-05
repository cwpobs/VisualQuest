using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
    public GameObject selectedObject;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
        if (targetObject)
        {
            selectedObject = targetObject.transform.gameObject;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            selectedObject = null;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject.GetComponent<RoomObject>().ExecuteRoomObject();
            
        }

   
    }
}
