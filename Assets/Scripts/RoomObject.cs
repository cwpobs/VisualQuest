using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomObject : MonoBehaviour
{
    public enum RoomObjectType { CollectibleItem, TextMessage, Door};
    public RoomObjectType objectType;
    public string DoorSceneName;

    public void ExecuteRoomObject()
    {
        switch (objectType)
        {
            case RoomObjectType.Door:
                Debug.Log("Executed " + gameObject.name);
                SceneManager.LoadScene(DoorSceneName);
                break;

                
        }
        
    }
}
