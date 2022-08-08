using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomObject : MonoBehaviour
{
    public string displayName;
    public enum RoomObjectType { InventoryItem, TextMessage, Door};
    public RoomObjectType objectType;
    public string DoorSceneName;
    public string MessageText;
    public int itemIndex;

    private RoomLogic roomLogic;

    private void Start()
    {
        roomLogic = GameObject.Find("RoomLogic").GetComponent<RoomLogic>(); 
        
    }
    public void ExecuteRoomObject()
    {
        switch (objectType)
        {
            case RoomObjectType.Door:
                SceneManager.LoadScene(DoorSceneName);
                break;
            case RoomObjectType.TextMessage:
                roomLogic.AddTextBlockMessage(MessageText);
                break;
            case RoomObjectType.InventoryItem:
                roomLogic.AddInventoryItem(itemIndex, displayName);
                roomLogic.AddTextBlockMessage(displayName + " отправляется в ваш инветарь");
                roomLogic.ShowInventory();
                Destroy(gameObject);
                break;


        }
        
    }
}
