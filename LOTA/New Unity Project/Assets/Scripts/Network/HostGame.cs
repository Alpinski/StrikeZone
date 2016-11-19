using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 6;

    private NetworkManager networkmanger;

    private string roomPassword = "";

    private string roomName;

    void start()
    {
        networkmanger = NetworkManager.singleton;
        if (networkmanger.matchMaker == null)
        {
            networkmanger.StartMatchMaker();
        }
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void CreateRoom()
    {
        if(roomName != "" && roomName != null)
        {
            Debug.Log("Creating Room: " + roomName + " with room for " + roomSize + " players.");
            // create room
           networkmanger.matchMaker.CreateMatch(roomName, roomSize, true, roomPassword,"","",0,0, networkmanger.OnMatchCreate);

        }
    }

}

