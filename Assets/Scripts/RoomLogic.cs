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
    // 2 - Курсор Действие
    // 3 - Курсор Взять предмет


    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;



    private Label textBlock;
    private Label stateString;

    private int textBlockLinesCount = 4;
    private string[] textBlockLines;
    private int textBlockCounter = 0;

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
       
            // Switch to 1920 x 1080 windowed
            Screen.SetResolution(1920 , 1080, false);


        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        textBlock = rootVisualElement.Q<Label>("TextBlock");
        stateString = rootVisualElement.Q<Label>("StateString");
        Inventory = new List<InventoryItem>(); 
        VisitedLocationLog = new List<string>();
        textBlockLines = new string[textBlockLinesCount];
        for (int i = 0; i < textBlockLines.Length; i++)
        {
            textBlockLines[i] = new string("");
        }

        textBlock.text = "\n\n";
    }

    public void AddTextBlockMessage(string message)
    {
        if (textBlockCounter < textBlockLinesCount)
        {
            textBlockLines[textBlockCounter] = message + "\n\n";
            textBlockCounter++;
            Debug.Log(textBlockCounter.ToString());
        }
        else
        {
            for (int j = 0; j < textBlockCounter - 1; j++)
            {
                textBlockLines[j] = textBlockLines[j + 1];

            }
            
            textBlockLines[textBlockCounter-1] = message + "\n\n";
        }

        textBlock.text = "";
        string colorCodeString = "#d9d9d9";
        for (int i = 0; i < textBlockCounter-1; i++)
        {
            textBlock.text += "<color=" + colorCodeString + ">"+textBlockLines[i]+"</color>";
        }

        colorCodeString = "#faff25";
        textBlock.text += "<color=" + colorCodeString + ">"+textBlockLines[textBlockCounter - 1]+"</color>";

    }
    public void AddInventoryItem(int _index, string _displayName)
    {
        InventoryItem item = new InventoryItem(_index, _displayName);
        Inventory.Add(item);
    }
    public void ShowInventory()
    {
        string invText = "";
        invText += "В вашем инвентаре сейчас:" + "\n";
        for (int i = 0; i < Inventory.Count; i++)
        {
            invText += Inventory[i].itemName + "\n";
        }

        AddTextBlockMessage(invText);
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
                    case RoomObject.RoomObjectType.GameExit:
                        UnityEngine.Cursor.SetCursor(cursorTextures[0], hotSpot, cursorMode);
                        break;
                    case RoomObject.RoomObjectType.TextMessage:
                        UnityEngine.Cursor.SetCursor(cursorTextures[1], hotSpot, cursorMode);
                    break;
                    case RoomObject.RoomObjectType.RedirectByItem:
                        UnityEngine.Cursor.SetCursor(cursorTextures[2], hotSpot, cursorMode);
                    break;
                    case RoomObject.RoomObjectType.InventoryItem:
                        UnityEngine.Cursor.SetCursor(cursorTextures[3], hotSpot, cursorMode);
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
