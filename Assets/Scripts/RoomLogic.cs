using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomLogic : MonoBehaviour
{
    public GameObject selectedObject;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;



    private Label textBlock;
    private Label stateString;

    private static RoomLogic roomLogicInstance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (roomLogicInstance == null)
        {
            roomLogicInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        textBlock = rootVisualElement.Q<Label>("TextBlock");
        stateString = rootVisualElement.Q<Label>("StateString");

        textBlock.text = "—тарт игры\n";
    }

    public void AddTextBlockMessage(string message)
    {
        textBlock.text += message + "\n";
    }
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
        if (targetObject)
        {
            selectedObject = targetObject.transform.gameObject;
            UnityEngine.Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); //добавить код выбора нужного курсора

            stateString.text = selectedObject.name;
        }
        else
        {
            selectedObject = null;
            UnityEngine.Cursor.SetCursor(null, Vector2.zero, cursorMode);
            stateString.text = "";
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject.GetComponent<RoomObject>().ExecuteRoomObject();
            
        }

   
    }
}
