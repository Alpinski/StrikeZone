using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

    private NetworkManager nm;

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    [SerializeField]
    private Text status;

	void Start ()
    {
        nm = NetworkManager.singleton;
        if (nm.matchMaker == null)
        {

            nm.StartMatchMaker();
        }

        Refresh();
	}

    public void Refresh()
    {

        ClearRooms();
        nm.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

   public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchlist)
    {
        status.text = "";

        if (roomList.Count == 0)
        {
            status.text = "No Rooms At The Moment.";
        }
        if (!success || matchlist == null)
        {
            status.text = "Couldn't get room list";
            return;
        }

        foreach (MatchInfoSnapshot match in matchlist)
        {
            GameObject roomListItemGo = Instantiate(roomListItemPrefab);
            roomListItemGo.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = roomListItemGo.GetComponent<RoomListItem>();
            if(_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }

            roomList.Add(roomListItemGo);

        }


    }
	

    void ClearRooms()
    {
        for(int i = 1; 1<roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        Debug.Log("Joining " + _match);
        nm.matchMaker.JoinMatch(_match.networkId, "","","",0,0, nm.OnMatchJoined);
        ClearRooms();
        status.text = "Joining ...";
    }

}
