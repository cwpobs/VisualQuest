using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomObject : MonoBehaviour
{
    public string displayName;
    public enum RoomObjectType { InventoryItem, TextMessage, Door, LocationInfo, RedirectByItem, GameExit};
    public RoomObjectType objectType;
    public string DoorSceneName;
    public string MessageText;
    public int itemIndex;
    public string itemName;

    private RoomLogic roomLogic;
    private float timer = 0.1f;

    private void Start()
    {
        roomLogic = GameObject.Find("RoomLogic").GetComponent<RoomLogic>();
        
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            timer = 0.0f;
            LateStart();
        }
    }
    private void LateStart()
    {
        
        switch (objectType)
        {
            case RoomObjectType.LocationInfo:
                bool locVisited = false;
                for (int i = 0; i < roomLogic.VisitedLocationLog.Count; i++)
                {
                    if (roomLogic.VisitedLocationLog[i] == SceneManager.GetActiveScene().name)
                    {
                        locVisited = true;
                    }
                }

                if (locVisited)
                {
                    Destroy(gameObject);
                }
                else
                {
                    roomLogic.AddTextBlockMessage(MessageText);
                    roomLogic.VisitedLocationLog.Add(SceneManager.GetActiveScene().name);
                    Destroy(gameObject);
                }
                break;

            case RoomObjectType.InventoryItem:
                bool itemCollected = false;
                for (int i = 0; i < roomLogic.Inventory.Count; i++)
                {
                    if (roomLogic.Inventory[i].index == itemIndex)
                    {
                        itemCollected = true;
                    }
                }

                if (itemCollected)
                {
                    Destroy(gameObject);
                }
                break;
        }

    }
    public void ExecuteRoomObject()
    {
        switch (objectType)
        {
            case RoomObjectType.GameExit:
                Application.Quit();
                break;
            case RoomObjectType.Door:
                SceneManager.LoadScene(DoorSceneName);
                break;
            case RoomObjectType.TextMessage:
                roomLogic.AddTextBlockMessage(MessageText);
                break;
            case RoomObjectType.InventoryItem:
                roomLogic.AddInventoryItem(itemIndex, itemName);
                roomLogic.AddTextBlockMessage(itemName + " отправляется в ваш инветарь");
                roomLogic.ShowInventory();
                Destroy(gameObject);
                break;
            case RoomObjectType.RedirectByItem:
                int foundItemIndex = -1;
                for (int i = 0; i < roomLogic.Inventory.Count; i++)
                {
                    if (roomLogic.Inventory[i].index == itemIndex)
                    {
                        foundItemIndex = i;

                    }
                }
                if (foundItemIndex > -1)
                {
                    roomLogic.AddTextBlockMessage("Вы используете " + roomLogic.Inventory[foundItemIndex].itemName);
                    roomLogic.Inventory.RemoveAt(foundItemIndex);
                    SceneManager.LoadScene(DoorSceneName);

                }
                else
                {
                   roomLogic.AddTextBlockMessage(MessageText);
                }
                break;



        }
        
    }
}
