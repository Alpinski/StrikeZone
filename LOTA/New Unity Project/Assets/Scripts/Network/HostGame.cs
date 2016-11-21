using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 4;

    private NetworkManager networkmanger;

    private string roomName;

    public Canvas canvas;


    void Start()
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

            if (networkmanger != null)
            {
                Debug.Log("Creating Room: " + roomName + " with room for " + roomSize + " players.");
                // create room
                networkmanger.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkmanger.OnMatchCreate);
                canvas.enabled = false;
            }
        }
    }

}

