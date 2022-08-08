using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomObject : MonoBehaviour
{
    public string displayName;
    public enum RoomObjectType { InventoryItem, TextMessage, Door, LocationInfo, RedirectByItem, DestroyByitem};
    public RoomObjectType objectType;
    public string DoorSceneName;
    public string MessageText;
    public int itemIndex;

    private RoomLogic roomLogic;
    private float timer = 1.0f;

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
                roomLogic.AddTextBlockMessage(MessageText);
                Destroy(gameObject);
                break;
        }

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
                roomLogic.AddTextBlockMessage(displayName + " ������������ � ��� ��������");
                roomLogic.ShowInventory();
                Destroy(gameObject);
                break;
            


        }
        
    }
}
