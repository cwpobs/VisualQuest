using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomObject : MonoBehaviour
{
    public enum RoomObjectType { CollectibleItem, TextMessage, Door};
    public RoomObjectType objectType;
    public string DoorSceneName;
    public string MessageText;

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


        }
        
    }
}
