using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomLogic : MonoBehaviour
{
    public struct InventoryItem
    {
        public int index;
        public string itemName;

        public InventoryItem(int _index, string _itemName)
        {
            index = _index;
            itemName = _itemName;
        }
    };
    public List<InventoryItem> Inventory;
    public List<string> VisitedLocationLog;

    public GameObject selectedObject;

    public Texture2D[] cursorTextures;
    // 0 - Курсор Дверь
    // 1 - Курсор Глаз


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
        Inventory = new List<InventoryItem>(); 
        VisitedLocationLog = new List<string>();

        textBlock.text = "Старт игры\n\n";
    }

    public void AddTextBlockMessage(string message)
    {
        textBlock.text += message + "\n\n";
    }
    public void AddInventoryItem(int _index, string _displayName)
    {
        InventoryItem item = new InventoryItem(_index, _displayName);
        Inventory.Add(item);
    }
    public void ShowInventory()
    {
        textBlock.text += "В вашем инвентаре сейчас:" + "\n";
        for (int i = 0; i < Inventory.Count; i++)
        {
            textBlock.text += Inventory[i].itemName + "\n";
        }
        textBlock.text += "\n";
    }
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
        if (targetObject)
        {
            selectedObject = targetObject.transform.gameObject;
            RoomObject RO;
            RO = selectedObject.GetComponent<RoomObject>();
            if (RO)
            {
                stateString.text = RO.displayName;
                switch (RO.objectType)
                {
                    case RoomObject.RoomObjectType.Door:
                        UnityEngine.Cursor.SetCursor(cursorTextures[0], hotSpot, cursorMode);
                    break;
                    case RoomObject.RoomObjectType.TextMessage:
                        UnityEngine.Cursor.SetCursor(cursorTextures[1], hotSpot, cursorMode);
                    break;
                }
                
            }



            
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
